using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

using gasstation.code.core.objects;
using gasstation.code.core.item;
using gasstation.code.core.player;
using gasstation.code.core.constants;
using gasstation.code.core.dialogue;

namespace gasstation.code.gameplay.inventory
{
    public class InventoryHubController : GasStationChildSingleton
    {
        public static InventoryHubController GetInventoryHubController()
        {
            return (InventoryHubController)InventoryHubController.getInstance("InventoryHubController");
        }

        public override void AfterGasStationStart()
        {
            this.InitInventoryUI();
        }

        public static void EmptySlotClick()
        {
            DialogueManagement.GetDialogueManager().PrintSentence("Empty Item slot..");
        }

        public void InitInventoryUI()
        {
            if (InventoryController.GetInventoryController().isOpen)
            {
                int gameObjectsOffset = 0;
                GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("InventoryItem");
                GameObject[] gameObjectsText = GameObject.FindGameObjectsWithTag("InventoryItemTxt");

                Player player = Player.GetPlayer();
                List<Item> inv = player.GetInventory();

                for (int i = 0; i < inv.Count; i++)
                {
                    gameObjects[i].GetComponent<Button>().onClick.AddListener(inv[i].OnClickItem);

                    // Apply whatever available sprite to the inventory hub UI
                    gameObjects[i].GetComponent<Image>().sprite = Sprites.LookupSprite(inv[i].spriteName);

                    Text textDescription = gameObjectsText[i].GetComponent<Text>();
                    textDescription.text = inv[i].name;
                    textDescription.SetAllDirty();
                    gameObjectsOffset++;
                }

                for (int i = gameObjectsOffset; i < gameObjects.Length; i++)
                {
                    Text textDescription = gameObjectsText[i].GetComponent<Text>();
                    textDescription.text = "Empty";
                    gameObjects[i].GetComponent<Button>().onClick.AddListener(InventoryHubController.EmptySlotClick);
                }
            }
        }
    }
}