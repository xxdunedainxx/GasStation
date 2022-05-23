using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using gasstation.code.core.clicking;
using gasstation.code.core.ui.intereactable;
using gasstation.code.core.objects;
using gasstation.code.core.logging;
using gasstation.code.core.constants;


namespace gasstation.code.core.ui.intereactable
{
    public class InteractClickable : GasStationChild, IClickable, IInteractable
    {
        private bool registerUnlocked = true;
        public float spriteRadius = 0;
        public Action additionalInteractEvent = null;


        public void ClearAdditionalInteractEvent()
        {
            this.additionalInteractEvent = null;
        }

        public float ValueBasedOnRadiusPercent(float rPercent)
        {
            return rPercent * this.spriteRadius;
        }

        private void CalculateSpriteRadius()
        {
            this.spriteRadius = this.gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;
            this.gameObject.GetComponent<BoxCollider2D>().size.Set(this.spriteRadius, this.spriteRadius);
        }

        public bool CheckIfUIClicked()
        {
            return GasStation.GetEventSystem().IsPointerOverGameObject();
        }

        public bool CanInteract()
        {
            float v = this.ValueBasedOnRadiusPercent(.1f);
            Collider2D interactChecks = Physics2D.OverlapCircle(
                this.transform.position,
                 v,
                Layers.PLAYER_LAYER
            );
            if (interactChecks != null && !CheckIfUIClicked())
            {
                LogFactory.INFO("Can interact");
                return true;
            }
            LogFactory.INFO("Cannot interact :(");
            return false;
        }

        public void click()
        {
            if (this.CanInteract())
            {
                if(this.additionalInteractEvent != null)
                {
                    this.additionalInteractEvent();
                }
                this.OnInteract();
            } 
        }

        public bool ClickEnabled()
        {
            return this.registerUnlocked;
        }

        public virtual void OnInteract()
        {

        }

        public override void AfterStart()
        {
            this.CalculateSpriteRadius();
        }
    }
}