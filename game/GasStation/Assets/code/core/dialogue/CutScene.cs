using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using gasstation.code.core.objects;
using gasstation.code.core.logging;

using gasstation.code.core.player;

namespace gasstation.code.core.dialogue
{
    public class CutScene : GasStationChild
    {
        public List<Dialogue> dialogueMessages;

        public override void AfterStart()
        {
            
        }

        /*
         * Runs the Cut Scene
         */ 
        public virtual void RunCutScene()
        {
            // Freeze player
            Player.GetPlayer().FreezePlayer();

            // Execute Dialogues
            DialogueManagement.GetDialogueManager().StartDialogue(this.dialogueMessages, endDialogueCallback: this.EndCutScene);
        }

        /*
         * Callback run after cutscene completes
         */
        public virtual void EndCutScene()
        {
            Player.GetPlayer().UnfreezePlayer();
        }
    }
}
