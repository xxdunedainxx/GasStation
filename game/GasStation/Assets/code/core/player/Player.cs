using UnityEngine;
using gasstation.code.core.objects;
using gasstation.code.core.logging;
using gasstation.code.core.item;
using gasstation.code.gameplay.inventory;

using gasstation.code.core.dialogue;
using gasstation.code.core.attributes;
using System.Collections.Generic;

namespace gasstation.code.core.player
{
    public class Player : GasStationChildSingleton
    {
        public Rigidbody2D body;
        public Transform position;
        public PlayerController controller;
        private GameObject playerFeet;
        private List<Item> inventory = new List<Item>();
        private Attributes attributes = null;
        private Item equipedItem = null;
        private static bool IsReady = false;

        public static Player GetPlayer()
        {
            return (Player)Player.getInstance("Player");
        }

        public Item EquippedItem()
        {
            return this.equipedItem;
        }

        /* ATTRIBUTE METHODS    
         * 
         * 
         * 
         */

        public bool ItemEquipped()
        {
            return this.equipedItem != null;
        }

        public void Equip(Item item)
        {
            this.equipedItem = item;
        }

        public void UnEquip()
        {
            this.equipedItem = null;
        }
        
        private void AttributeChangeDialogue(string attributeChanged,string gainOrLoss, int value)
        {
            Dialogue attributeChangeDialogue = new Dialogue($"You {gainOrLoss} {value.ToString()} {attributeChanged}");
            DialogueManagement.GetDialogueManager().AddDialogueMessageToBack(attributeChangeDialogue);
        }
        
        public void Heal(int healing)
        {
            this.attributes.Heal(healing);
        }

        public void Damage(int dmg)
        {
            this.attributes.Damage(dmg);
        }

        public void UnTrust(int uTrustValue)
        {
            this.attributes.UnTrust(uTrustValue);
            this.AttributeChangeDialogue("Trust", "loss",uTrustValue);
        }

        public void Trust(int uTrustValue)
        {
            this.attributes.Trust(uTrustValue);
            this.AttributeChangeDialogue("Trust", "gained", uTrustValue);
        }


        public void AddToInventory(Item itemToAdd)
        {
            LogFactory.INFO($"Adding item {itemToAdd.name} to player inventory");
            this.inventory.Add(itemToAdd);
            DialogueManagement.GetDialogueManager().PrintSentence($"You obtained {itemToAdd.name}");
            InventoryHubController.GetInventoryHubController().InitInventoryUI();
        }

        public void RemoveFromInventory(string itemToRemove)
        {
            LogFactory.INFO($"Removing item {itemToRemove} from player inventory");
            for (int i = 0; i < this.inventory.Count; i++)
            {
                if (this.inventory[i].name == itemToRemove)
                {
                    this.inventory.RemoveAt(i);
                    LogFactory.INFO("Item removed...");
                    DialogueManagement.GetDialogueManager().PrintSentence($"You lost {itemToRemove}");
                    break;
                }
            }
            InventoryHubController.GetInventoryHubController().InitInventoryUI();
        }

        public void FreezePlayer()
        {
            this.controller.FreezePlayer();
        }

        public void UnfreezePlayer()
        {
            this.controller.UnFreezePlayer();
        }

        public bool PlayerIsFrozen()
        {
            return this.controller.AllowPlayerMovement();
        }

        public void SetPlayerInventory(List<Item> items)
        {
            this.inventory = items;
        }

        public void PrintInventory()
        {
            LogFactory.INFO("player inventory::");
            foreach (Item i in this.inventory)
            {
                LogFactory.INFO($"Item: {i.name}");
            }
        }

        public Attributes GetAttributes()
        {
            return this.attributes;
        }

        public List<Item> GetInventory()
        {
            return this.inventory;
        }

        public bool PlayerHasItem(string item)
        {
            foreach (Item i in this.inventory)
            {
                if (i.name == item)
                {
                    return true;
                }
            }
            return false;
        }

        private void InitAttributes()
        {
            if(this.attributes == null)
            {
                this.attributes = new Attributes(
                    new List<Attribute>()
                    {
                        new Trust(5),
                        new StreetCred(5),
                        new Health(5),
                    },
                    "player"
                );

            }
        }

        public override void AfterGasStationStart()
        {
            LogFactory.INFO("Start player object");
            this.body = this.gameObject.GetComponent<Rigidbody2D>();
            this.InitPlayerController();
            this.InitAttributes();
            Player.IsReady = true;
        }

        private void InitPlayerController()
        {
            //this.gameObject.AddComponent<PlayerController>();
            this.controller = this.gameObject.GetComponent<PlayerController>();
            this.controller.feet = PlayerTransforms.feet;
            //this.controller.feet = PlayerTransforms.feet.transformToMove;
            this.controller.rb = this.body;
            this.controller.UnFreezePlayer();
        }

        public static bool IsPlayerReady()
        {
            return Player.IsReady;
        }

    }
    public class PlayerTransforms
    {
        public static Transform feet;
    }
}
