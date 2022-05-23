using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using gasstation.code.core.player;
using gasstation.code.core.item;
using gasstation.code.core.logging;
using gasstation.code.core.dialogue;
using gasstation.code.core.constants;

namespace gasstation.code.core.ui.intereactable
{
    public class FloorSpill : InteractClickable
    {
        public override void OnInteract()
        {
            Item playerItem = Player.GetPlayer().EquippedItem();

            if (playerItem != null && playerItem.name == "mop")
            {
                LogFactory.INFO("Cleaning the spill :)");
                // remove from game
                Destroy(this.gameObject);
            }
        }

        private void DetermineFloorSpillSprite()
        {
            float floorSpillIndex = Random.Range(1,3);
            if(floorSpillIndex == 1)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = Sprites.LookupSprite(Sprites.floorSpill1);
            } else
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = Sprites.LookupSprite(Sprites.floorSpill2);
            }
        }

        public override void AfterStart()
        {
            base.AfterStart();
            this.DetermineFloorSpillSprite();
        }
    }
}
