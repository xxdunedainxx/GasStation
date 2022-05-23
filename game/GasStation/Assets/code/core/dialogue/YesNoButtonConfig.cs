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

    public class YesNoButtonConfig
    {
        public string yesText = "Yes";
        public string noText = "No";
        public List<Dialogue> yesSelectionDialogue = new List<Dialogue> { new Dialogue("you selected yes") };
        public List<Dialogue> noSelectionDialogue = new List<Dialogue> { new Dialogue("you selected no") };

        public YesNoButtonConfig(string yText = "Yes", string nText = "No")
        {
            this.yesText = yText;
            this.noText = nText;
        }

        public YesNoButtonConfig(List<Dialogue> yesSelection, List<Dialogue> noSelection, string yText = "Yes", string nText = "No")
        {
            this.yesSelectionDialogue = yesSelection;
            this.noSelectionDialogue = noSelection;
            this.yesText = yText;
            this.noText = nText;
        }

        public void ClearDialogues()
        {
            this.noSelectionDialogue.Clear();
            this.yesSelectionDialogue.Clear();
        }

        public virtual void Yes()
        {
            LogFactory.INFO("yes clicked");
            foreach(Dialogue d in this.yesSelectionDialogue)
            {
                DialogueManagement.GetDialogueManager().AddDialogueMessageToFront(d);
            }
            //this.ClearDialogues();
            DialogueManagement.GetDialogueManager().DisplayNextSentence();
        }

        public virtual void No()
        {
            LogFactory.INFO("no clicked");
            foreach (Dialogue d in this.noSelectionDialogue)
            {
                DialogueManagement.GetDialogueManager().AddDialogueMessageToFront(d);
            }
            this.ClearDialogues();
            DialogueManagement.GetDialogueManager().DisplayNextSentence();
        }
    }
}