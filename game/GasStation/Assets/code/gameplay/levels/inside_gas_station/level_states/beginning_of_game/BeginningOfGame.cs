using gasstation.code.core.npc;
using gasstation.code.core.level;
using gasstation.code.core.dialogue;
using gasstation.code.core.player;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using gasstation.code.core.npc.bear;
using gasstation.code.core.ui.intereactable;
using gasstation.code.core.item;
using gasstation.code.core.data;

namespace gasstation.code.gameplay.levels
{
    public class BeginningOfGame : LevelState
    {
        public bool BearTrainingActive = false;
        public bool CleaningActive = false;
        private LevelStatePersistence state;

        private static Dictionary<string, string> EventToggles = new Dictionary<string, string>()
        {
            { "MilkCall", "" },
        };

        CashRegister register;

        private static List<Dialogue> OscarStartDialogue = new List<Dialogue>
        {
            new Dialogue("First things first, let me show you how to use the register, on the counter.")
        };

        private static List<Dialogue> OscarClickRegisterDialogue = new List<Dialogue>
        {
            new Dialogue("Good job.. now lets get started with the register"),
            new Dialogue("Great work, once your done fucking around with the register come back over here, i have something to show you.")
        };

        private static List<Dialogue> PhoneCallStartDialogue = new List<Dialogue>
        {
            new Dialogue("So this right here is my secret to staying up all nigh--"),
            new Dialogue("RINGGGGGGGGGGGGGG"),
            new Dialogue("Oh. Shit, someone's on the phone. This is a good opportunity for you to learn some customer service."),
            new Dialogue("Hop on over and pick up the phone")
        };

        // Phone Call Dialogue Tree
        private static List<Dialogue> PhoneCallDialogue = new List<Dialogue>
        {
            new YesNoDialogue(
                "Hey is this Joe's gas station?",
                new YesNoButtonConfig(
                    yesSelection: new List<Dialogue> {
                        new YesNoDialogue(
                            "o yah? Do you all have any milk in stock right now?",
                            new YesNoButtonConfig(
                                yesSelection: new List<Dialogue> {
                                    new ActionableDialogue(
                                        "Alright awesome, i'll be right over",
                                        BeginningOfGame.PhoneCallYesMilk
                                    )
                                },
                                 noSelection: new List<Dialogue> {
                                    new ActionableDialogue(
                                        "bummer..",
                                        BeginningOfGame.PhoneCallNoMilk
                                    )
                                },
                                yText: "I think so?",
                                nText:"I have no idea, but probably not."
                            )
                        )
                        },
                    noSelection: new List<Dialogue> {
                        new ActionableDialogue(
                            "bummer...",
                            actionToRun: BeginningOfGame.PhoneCallEarlyNoSelection
                        )
                    }
                    )
            )
        };

        public static BeginningOfGame InitBeginningOfGameState()
        {
            return new BeginningOfGame();
        }

        public static BeginningOfGame GetBeginningOfGameState()
        {
            return (BeginningOfGame)BeginningOfGame.getInstance("BeginningOfGame");
        }

        public void OnFirstRegisterClick()
        {
            DialogueManagement.GetDialogueManager().StartDialogue(BeginningOfGame.OscarClickRegisterDialogue);
            this.register.ClearAdditionalInteractEvent();
            // After the register training is over, go over phone answering
            this.SetupPhoneEvent();
        }

        private void SetupPhoneEvent()
        { 
            Oscar.GetOscar().SetDialogue(BeginningOfGame.PhoneCallStartDialogue);
            Phone.GetPhone().QueuePhonecall(BeginningOfGame.PhoneCallDialogue);
        }

        public static void PhoneCallYesMilk()
        {
            DialogueManagement.GetDialogueManager().PrintSentence("WHY WOULD YOU TELL HIM WE HAVE MILK");
            List<Dialogue> oscarDialogue = new List<Dialogue>
            {
                new ActionableDialogue("We dont have milk you idiot", actionToRun: ActivateBearTraining),
                new Dialogue("Alright now I'd like to introduce to my best friend in the world, Bear."),
                new Dialogue("Press 'B' to bring up Bear's command console")
            };
            BeginningOfGame.EventToggles["MilkCall"] = "PhoneCallYesMilk";
            Oscar.GetOscar().SetDialogue(oscarDialogue);
        }
        public static void PhoneCallNoMilk()
        {
            DialogueManagement.GetDialogueManager().PrintSentence("alright phew, yah we are out of milk");
            List<Dialogue> oscarDialogue = new List<Dialogue>
            {
                new ActionableDialogue("Good job", actionToRun: ActivateBearTraining),
                new Dialogue("Alright now I'd like to introduce to my best friend in the world, Bear."),
                new Dialogue("Press 'B' to bring up Bear's command console")
            };
            Player.GetPlayer().Trust(1);
            Oscar.GetOscar().SetDialogue(oscarDialogue);
            BeginningOfGame.EventToggles["MilkCall"] = "PhoneCallNoMilk";
        }
        public static void PhoneCallEarlyNoSelection()
        {
            // Deprecate trust due to lie
            DialogueManagement.GetDialogueManager().PrintSentence("well you didnt handle that well");
            List<Dialogue> oscarDialogue = new List<Dialogue>
            {
                new ActionableDialogue("hmmm i dont know if we have milk or not...", actionToRun: ActivateBearTraining),
                new Dialogue("Alright now I'd like to introduce to my best friend in the world, Bear."),
                new Dialogue("Press 'B' to bring up Bear's command console")
            };
            Player.GetPlayer().UnTrust(1);
            Oscar.GetOscar().SetDialogue(oscarDialogue);
            BeginningOfGame.EventToggles["MilkCall"] = "PhoneCallEarlyNoSelection";
        }

        public static void ActivateBearTraining()
        {
            BeginningOfGame.GetBeginningOfGameState().BearTrainingActive = true;
        }

        public static void BearTraining()
        {
            // listen for 'b' click and prompt with info regarding bear training 
            BeginningOfGame.GetBeginningOfGameState().BearTrainingActive = false;
            List<Dialogue> bearConsoleDialogue = new List<Dialogue>
            {
                new Dialogue("To call bear over to you, press 'Over here bear!'"),
                new Dialogue("To get bear to follow you around, press 'Heel!'"),
                new Dialogue("To get bear to stop whatever he is currently doing, like heel, press 'Stay...'")
            };
            DialogueManagement.GetDialogueManager().StartDialogue(bearConsoleDialogue, endDialogueCallback: BeginningOfGame.CleaningTraining);
        }

        public static void CleaningTraining()
        {
            // Adds the mop to the players inv
            Player.GetPlayer().AddToInventory(new Mop());
            List<Dialogue> cleanTrainingDialogue = new List<Dialogue>
            {
                new Dialogue("Alright, now onto some of the less... glorious tasks.."),
                new Dialogue("This place can get extremely dirty. Heres a mop, try cleaning up the stuff on the floor. To view the mop in your inventory, press 'I'"),
                new Dialogue("To cleanup all of this stuff, go up to the mess, and left click the mess.")
            };
           Oscar.GetOscar().SetDialogue(cleanTrainingDialogue);
           BeginningOfGame.GetBeginningOfGameState().CleaningActive = true;
        }

        public static void MilkOutcome()
        {
            // milk phone call outcome, end of beginning of game first scene
            List<Dialogue> milkOutcome = new List<Dialogue>
            {
                new Dialogue("you've reached the end..")
            };
            DialogueManagement.GetDialogueManager().StartDialogue(milkOutcome);
        }

        void Update()
        {
            if(this.BearTrainingActive && BearCommandHandler.GetBearCommandHandler().IsCanvasOpen()){
                // Bear Training Dialogue
                BeginningOfGame.BearTraining();

                // De-toggle bear training
                this.BearTrainingActive = false;
            }

            if (this.CleaningActive && !Cleaning.CleaningRequired())
            {
                this.CleaningActive = false;
                BeginningOfGame.MilkOutcome();
            }
        }


        public override void LevelStateStart()
        {
            base.LevelStateStart();
            this.state = Persistence.FetchLevelStatePersistence(BeginningOfGame.BEGINNING_OF_GAME);
            this.register = CashRegister.GetRegister();
            this.register.additionalInteractEvent = this.OnFirstRegisterClick;

            CutScene scene = new CutScene();
            scene.dialogueMessages = new List<Dialogue>
            {
                new Dialogue("Rod. Your fucking late"),
                new Dialogue("Get over to the front counter, I need to get you trained quick.")
            };
            scene.RunCutScene();

            Oscar.GetOscar().controller.RelevantVectorMove(-.25f, 0, endMovementCallback: Oscar.GetOscar().OrientSouth);
            Oscar.GetOscar().SetDialogue(BeginningOfGame.OscarStartDialogue);
        }

        public override void AttachToMainGame()
        {
            GasStation.GetGame().gameObject.AddComponent<BeginningOfGame>();
        }
    }
}
