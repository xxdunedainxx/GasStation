using Newtonsoft.Json;
using gasstation.code.core.player;
using gasstation.code.core.dialogue;
using gasstation.code.core.constants;
using UnityEngine;
using System;

namespace gasstation.code.core.item
{
    [System.Serializable]
    public class Mop : Item
    {
        public Mop() : base(
            "mop",
            "", 
            false, 
            0, 
           "Just a basic mop")
        {
        }

        public override void OnClickItem()
        {
            DialogueManagement.GetDialogueManager().PrintSentence($"You Equipped a mop");
            Player.GetPlayer().Equip(this);
        }
    }
}