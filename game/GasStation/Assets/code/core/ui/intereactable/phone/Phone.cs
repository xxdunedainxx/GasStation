using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using gasstation.code.core.clicking;
using gasstation.code.core.ui.intereactable;
using gasstation.code.core.objects;
using gasstation.code.core.logging;
using gasstation.code.core.constants;
using gasstation.code.gameplay.inventory;
using gasstation.code.core.dialogue;

namespace gasstation.code.core.ui.intereactable
{
    public class Phone : InteractClickableSingleton
    {
        List<Dialogue> currentPhoneDialogue = null;
        public static List<Dialogue> noAnswerDialogue = new List<Dialogue>(){ new Dialogue("*** VERY UGLY STATIC ***")};

        public static Phone GetPhone()
        {
            return (Phone)Phone.getInstance("Phone");
        }

        public override void OnInteract()
        {
            LogFactory.INFO("opening phone UI");
            this.AnswerPhone();
        }

        public void QueuePhonecall(List<Dialogue> phoneDialogue)
        {
            this.currentPhoneDialogue = phoneDialogue;
        }

        public void AnswerPhone()
        {
           if(this.currentPhoneDialogue != null)
            {
                DialogueManagement.GetDialogueManager().StartDialogue(this.currentPhoneDialogue);
            } else
            {
                DialogueManagement.GetDialogueManager().StartDialogue(Phone.noAnswerDialogue);
            }
        }
    }
}