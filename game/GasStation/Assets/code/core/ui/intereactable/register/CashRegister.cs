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

namespace gasstation.code.core.ui.intereactable
{
    public class CashRegister : InteractClickableSingleton
    {
        [SerializeField]
        Canvas registerCanvas;

        public static CashRegister GetRegister()
        {
            return (CashRegister)CashRegister.getInstance("CashRegister");
        }

        public override void OnInteract()
        {
            LogFactory.INFO("opening register");
            this.EnableRegisterUI();
            if (InventoryController.GetInventoryController().isOpen)
            {
                LogFactory.INFO("Closing inventory...");
                InventoryController.GetInventoryController().Close();
            }
        }

        public void EnableRegisterUI()
        {
            this.registerCanvas.gameObject.SetActive(true);
        }

        public void DisableRegisterUI()
        {
            this.registerCanvas.gameObject.SetActive(false);
        }
    }
}