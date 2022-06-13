using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using gasstation.code.core.clicking;
using gasstation.code.core.ui.intereactable;
using gasstation.code.core.objects;
using gasstation.code.core.logging;
using gasstation.code.core.constants;
using gasstation.code.core.data;
using gasstation.code.gameplay.inventory;

namespace gasstation.code.core.ui.intereactable
{
    public class CashRegister : InteractClickableSingleton
    {
        [SerializeField]
        Canvas registerCanvas;
        [SerializeField]
        Text cashText;
        [SerializeField]
        Text cashDeficit;
        [SerializeField]
        Text cashStolen;
        [SerializeField]
        Text bullets;

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

        private void DatabindRegisterValues()
        {
            this.cashText.text = Persistence.FetchRegisterState().totalCash.ToString();
            this.cashDeficit.text = Persistence.FetchRegisterState().deficitCash.ToString();
            this.cashStolen.text = Persistence.FetchRegisterState().cashStolen.ToString();
            this.bullets.text = Persistence.FetchRegisterState().bullets.ToString();
        }

        public void EnableRegisterUI()
        {
            this.DatabindRegisterValues();
            this.registerCanvas.gameObject.SetActive(true);
        }

        public void DisableRegisterUI()
        {
            this.registerCanvas.gameObject.SetActive(false);
        }
    }
}