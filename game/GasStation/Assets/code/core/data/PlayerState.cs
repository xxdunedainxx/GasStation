using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using gasstation.code.core.logging;
using gasstation.code.core.player;
using gasstation.code.core.item;
using gasstation.code.core.attributes;

namespace gasstation.code.core.data
{
    // tracks things like status, inventory etc
    [System.Serializable]
    public class PlayerState
    {
        [JsonProperty("playerInventory")]
        public List<Item> playerInventory;

        [JsonProperty("playerAttributes")]
        public Attributes playerAttributes;

        [JsonIgnore]
        private static PlayerState instance = null;

        [JsonIgnore]
        public static PlayerState Instance
        {
            get
            {
                if (PlayerState.instance == null)
                {
                    PlayerState.instance = new PlayerState();
                }
                return PlayerState.instance;
            }
        }

        public PlayerState()
        {
            return;
        }

        public void UpdatePlayerState()
        {
            this.UpdatePlayerInventory();
            this.UpdatePlayerAttributes();
        }

        public void UpdatePlayerAttributes()
        {
            Player p = Player.GetPlayer();
            if( p!= null)
            {
                this.playerAttributes = p.GetAttributes();
            }
        }


        public void UpdatePlayerInventory()
        {
            Player p = Player.GetPlayer();
            if (p != null)
            {
                this.playerInventory = p.GetInventory();
            }
        }
    }
}
