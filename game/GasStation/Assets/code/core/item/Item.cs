using Newtonsoft.Json;
using gasstation.code.core.player;
using gasstation.code.core.dialogue;
using gasstation.code.core.constants;
using UnityEngine;
using System;

namespace gasstation.code.core.item
{
    [System.Serializable]
    public class Item
    {
        public readonly static string defaultSpriteName = Sprites.defaultItemSpriteName;
        public readonly static string defaultDescription = "Its an item?";
        public string description = "Its an item?";
        public string name;
        public string spriteName = "unknown";
        // not all items have 'durability' 
        public bool isDurable = false;
        public bool broken = false;
        public float durability = 0;

        public Item(string name)
        {
            this.name = name;
        }

        [JsonConstructor]
        public Item(string name, string sprite, bool durable, float durability, string description = "Its an item?")
        {
            this.description = description;
            this.name = name;
            this.spriteName = sprite;
            this.isDurable = durable;
            this.durability = durability;
        }

        public void PlayerObtain()
        {
            Player.GetPlayer().AddToInventory(this);
        }

        public void RemoveFromPlayerInventory()
        {
            Player.GetPlayer().RemoveFromInventory(this.name);
        }

        public void ReduceDurability(float reduceValue)
        {
            if (this.isDurable)
            {
                this.durability -= reduceValue;
                if (this.durability <= 0)
                {
                    this.RemoveFromPlayerInventory();
                    this.broken = true;
                }
            }
        }

        public virtual void OnClickItem()
        {
            this.PrintItemInfo();
        }

        public virtual void PrintItemInfo()
        {
            DialogueManagement.GetDialogueManager().PrintSentence($"{this.name}: {this.description}");
        }
    }
}
