using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using System;
using gasstation.code.core.objects;
using gasstation.code.core.logging;
using UnityEngine.UI;
using UnityEngine.Events;

namespace gasstation.code.core.dialogue
{

    public class DialogueManagement : SingletonMonobehaviour
    {
        [SerializeField]
        public Canvas dialogueCanvas;
        [SerializeField]
        public Button nextSentenceButton;
        [SerializeField]
        public Button closeDialogueButton;
        [SerializeField]
        public Button yesButton;
        [SerializeField]
        public Button noButton;
        [SerializeField]
        public Text yesButtonText;
        [SerializeField]
        public Text noButtonText;


        private List<Dialogue> dialogueMessages;
        private List<string> sentences;
        public static DialogueManagement instance { get; private set; }
        private DialogueWriter writer;
        private static bool isReady = false;
        private Action endDialogueCallBack = null;

        public static DialogueManagement GetDialogueManager()
        {
            return (DialogueManagement)DialogueManagement.getInstance("DialogueManagement");
        }

        public DialogueManagement()
        {

        }

        public override void AfterStart()
        {
            StartCoroutine(this.WaitForDependencies());
        }

        public void Update()
        {

        }

        public void SetWriterToEnd()
        {
            this.writer.SetToEnd();
        }

        private void AfterDependencies()
        {
            this.writer = (DialogueWriter)DialogueWriter.getInstance("DialogueWriter");
            DialogueManagement.isReady = true;
            this.sentences = new List<string>();
            this.dialogueMessages = new List<Dialogue>();
            this.Disable();
            this.AddButtonListeners();
        }

        private void CloseAndEndDialogue()
        {
            this.EndDialogue();
            this.Disable();
        }

        private void AddButtonListeners()
        {
            this.closeDialogueButton.onClick.AddListener(this.CloseAndEndDialogue);
            this.nextSentenceButton.onClick.AddListener(this.DisplayNextSentence);
        }

        private bool CheckDependencies()
        {
            return (
                DialogueWriter.isReady
            );
        }

        IEnumerator WaitForDependencies()
        {
            yield return new WaitUntil(this.CheckDependencies);
            this.AfterDependencies();
        }

        public void PrintSentence(string sentence, float printTime = .1f)
        {
            if (this.dialogueCanvas.gameObject.activeSelf == false)
            {
                this.Enable();
            }
            if (this.writer.AutoCompletedLast())
            {
                this.writer.UnsetAutoComplete();
            }

            if (this.writer.IsWriting())
            {
                this.writer.SetToEnd();
            }
            else
            {
                this.writer.PrintSentence(sentence, printTime);
            }
        }

        public void PrintSentence(Dialogue sentence, float printTime = 0.1f)
        {
            this.PrintSentence(sentence.Sentence(), printTime);
        }

        public static bool IsReady()
        {
            return DialogueWriter.isReady && DialogueManagement.isReady;
        }

        public void DisplayNextSentence()
        {
            if (this.writer.IsWriting())
            {
                this.writer.SetToEnd();
            }
            else
            {
                if (this.sentences.Count > 0)
                {
                    string sentenceToPrint = this.sentences[0];
                    this.sentences.RemoveAt(0);
                    this.PrintSentence(sentenceToPrint);
                }
                else if (this.dialogueMessages.Count > 0)
                {
                    Dialogue sentenceToPrint = this.dialogueMessages[0];
                    this.dialogueMessages.RemoveAt(0);
                    this.PrintSentence(sentenceToPrint);
                }
                else
                {
                    LogFactory.INFO("Oh no, no messages to print :(");
                    this.CloseAndEndDialogue();
                }
            }
        }

        private void Enable()
        {
            this.dialogueCanvas.gameObject.SetActive(true);
        }

        private void Disable()
        {
            this.dialogueCanvas.gameObject.SetActive(false);
        }

        public void AddDialogueMessageToFront(Dialogue dialogueToAdd)
        {
            this.dialogueMessages.Insert(0, dialogueToAdd);
        }

        public void AddDialogueMessageToBack(Dialogue dialogueToAdd)
        {
            this.dialogueMessages.Add(dialogueToAdd);
        }

        public void StartDialogue(List<Dialogue> dialogues, Action endDialogueCallback = null, bool yesNoButtonsEnabled = false, YesNoButtonConfig buttonConfig = null)
        {
            this.PrepareDialogue(endDialogueCallback, yesNoButtonsEnabled, buttonConfig);
            foreach (Dialogue dialogue in dialogues)
            {
                dialogueMessages.Add(dialogue);
            }

            this.DisplayNextSentence();
        }


        public void StartDialogue(List<string> sentences, Action endDialogueCallback = null, bool yesNoButtonsEnabled = false, YesNoButtonConfig buttonConfig = null)
        {
            this.PrepareDialogue(endDialogueCallback, yesNoButtonsEnabled, buttonConfig);
            foreach (string sentence in sentences)
            {
                this.sentences.Add(sentence);
            }

            this.DisplayNextSentence();
        }

        public void PrepareDialogue(Action endDialogueCallback, bool yesNoButtonsEnabled, YesNoButtonConfig buttonConfig = null)
        {
            this.EndDialogue();
            this.endDialogueCallBack = endDialogueCallback;

            if (yesNoButtonsEnabled)
            {
                this.PrepareYesNoButtons(buttonConfig);
            }
            else
            {
                this.DisableYesNoButtons();
            }
            this.Enable();
        }

        private void Close()
        {

        }

        public void DisableYesNoButtons()
        {
            this.yesButton.gameObject.SetActive(false);
            this.noButton.gameObject.SetActive(false);
            this.yesButtonText.gameObject.SetActive(false);
            this.noButtonText.gameObject.SetActive(false);

            this.yesButtonText.text = "Yes";
            this.noButtonText.text = "No";

            this.yesButton.onClick.RemoveAllListeners();

            this.noButton.onClick.RemoveAllListeners();
        }

        public void PrepareYesNoButtons(YesNoButtonConfig conf)
        {
            this.yesButton.gameObject.SetActive(true);
            this.noButton.gameObject.SetActive(true);
            this.yesButtonText.gameObject.SetActive(true);
            this.noButtonText.gameObject.SetActive(true);

            this.yesButton.onClick.RemoveAllListeners();
            this.yesButton.onClick.AddListener(conf.Yes);

            this.noButton.onClick.RemoveAllListeners();
            this.noButton.onClick.AddListener(conf.No);

            this.yesButtonText.text = conf.yesText;
            this.noButtonText.text = conf.noText;
        }

        public void EndDialogue()
        {
            LogFactory.INFO("finished dilgoue");
            this.sentences.Clear();
            this.dialogueMessages.Clear();
            this.writer.UnsetVars();
            this.writer.ClearTextBox();
            if (this.endDialogueCallBack != null)
            {
                Action endDialogueCache = this.endDialogueCallBack;
                endDialogueCallBack = null;
                LogFactory.INFO("calling end dialogue callback");
                endDialogueCache();
            }
        }
    }
}