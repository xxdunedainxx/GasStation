using UnityEngine;
using System;
using gasstation.code.core.objects;
using gasstation.code.core.logging;
using gasstation.code.core.item;
using gasstation.code.core.clicking;
using gasstation.code.core.constants;

using gasstation.code.core.dialogue;

using System.Collections.Generic;

namespace gasstation.code.core.npc
{

    public class Npc : GasStationChildSingleton, IClickable
    {
        [SerializeField]
        public string name;
        [SerializeField]
        List<Dialogue> currentDialogue;
        [SerializeField]
        public BoxCollider2D collider;
        [SerializeField]
        public Rigidbody2D rb;
        [SerializeField]
        public Animator animator;
        [SerializeField]
        public Transform position;

        public Action endDialogueCallback = null;

        public NpcController controller;
        public float spriteRadius = 0;

        public void SetDialogue(List<Dialogue> nDialogue, Action endDialogueCallback = null)
        {
            this.currentDialogue = nDialogue;
            this.endDialogueCallback = endDialogueCallback;
        }

        private void CalculateSpriteRadius()
        {
            this.spriteRadius = this.gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        }

        public override void AfterGasStationStart()
        {
            this.CalculateSpriteRadius();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void BeginNpcDialogue()
        {
            DialogueManagement.GetDialogueManager().StartDialogue(this.currentDialogue, endDialogueCallback: this.endDialogueCallback);
        }

        public float ValueBasedOnRadiusPercent(float rPercent)
        {
            return rPercent * this.spriteRadius;
        }

        public bool CheckPlayerCollision()
        {
            float v = this.ValueBasedOnRadiusPercent(2.0f);
            Collider2D interactChecks = Physics2D.OverlapCircle(
                this.transform.position,
                 v,
                Layers.PLAYER_LAYER
            );
            return interactChecks != null;
        }

        public bool CanInteract()
        {
            if (this.CheckPlayerCollision())
            {
                LogFactory.INFO("Can interact");
                return true;
            }
            LogFactory.INFO("Cannot interact :(");
            return false;
        }

        public virtual void click()
        {
            if (this.CanInteract())
            {
                DialogueManagement.GetDialogueManager().StartDialogue(this.currentDialogue);
            }
        }

        public bool ClickEnabled()
        {
            return true;
        }

        /*
         * Various orientation methods. Changes sprite to represent correct x/y axis direction
         */
        public virtual void OrientNorth()
        {

        }

        public virtual void OrientSouth()
        {

        }

        public virtual void OrientWest()
        {

        }

        public virtual void OrientEast()
        {

        }

        /*
         * Various orientation methods for animated movements
         */

        public virtual void OrientNorthAnimated()
        {

        }

        public virtual void OrientSouthAnimated()
        {

        }

        public virtual void OrientWestAnimated()
        {

        }

        public virtual void OrientEastAnimated()
        {

        }
    }

}