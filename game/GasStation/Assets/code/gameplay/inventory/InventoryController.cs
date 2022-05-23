using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using gasstation.code.core.objects;
using gasstation.code.core.logging;
using gasstation.code.core.dialogue;
using gasstation.code.core.data;

namespace gasstation.code.gameplay.inventory
{
    public class InventoryController : GasStationChildSingleton
    {
        [SerializeField]
        Canvas HubCanvas;
        [SerializeField]
        Image InventoryHub;
        [SerializeField]
        Canvas InventoryUI;
        [SerializeField]
        Button saveButton;
        [SerializeField]
        Button optionsButton;
        [SerializeField]
        Button inventoryButton;
        [SerializeField]
        Button closeInventoryButton;
        [SerializeField]
        Button exitInventoryHubButton;



        public static InventoryController GetInventoryController()
        {
            return (InventoryController)InventoryController.getInstance("InventoryController");
        }
        public bool isOpen = true;
        private bool ready = false;

        public override void AfterGasStationStart()
        {
            this.AddButtonListeners();
            LogFactory.INFO("Inventory controller is ready");
            this.ready = true;
            this.isOpen = this.enabled;
        }

        public void AddButtonListeners()
        {
            this.closeInventoryButton.onClick.AddListener(this.EnableMainHub);
            this.saveButton.onClick.AddListener(this.Save);
            this.inventoryButton.onClick.AddListener(this.Inventory);
            this.exitInventoryHubButton.onClick.AddListener(this.Close);
        }

        public bool IsReady()
        {
            return this.ready;
        }

        public void Open()
        {
            this.isOpen = true;
            this.HubCanvas.gameObject.SetActive(true);
        }

        public void Close()
        {
            this.isOpen = false;
            this.HubCanvas.gameObject.SetActive(false);
        }

        public void Save()
        {
            Persistence.Save();
            DialogueManagement.GetDialogueManager().PrintSentence("Game saved!", .001f);
        }

        public void DisableInventory()
        {
            this.InventoryUI.gameObject.SetActive(false);
        }

        public void Inventory()
        {
            this.DisableMainHub();
            this.InventoryUI.gameObject.SetActive(true);
            InventoryHubController.GetInventoryHubController().InitInventoryUI();
        }

        private void EnableMainHub()
        {
            this.saveButton.gameObject.SetActive(true);
            this.optionsButton.gameObject.SetActive(true);
            this.inventoryButton.gameObject.SetActive(true);
            this.closeInventoryButton.gameObject.SetActive(false);
            this.DisableInventory();
        }

        private void DisableMainHub()
        {
            this.saveButton.gameObject.SetActive(false);
            this.optionsButton.gameObject.SetActive(false);
            this.inventoryButton.gameObject.SetActive(false);
            this.closeInventoryButton.gameObject.SetActive(true);
        }
    }
}