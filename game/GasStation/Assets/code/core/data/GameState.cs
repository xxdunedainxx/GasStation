using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace gasstation.code.core.data
{
    /// <summary>
    ///  tracks things like current level, last level, 
    /// </summary>
    public static class GameModes
    {
        public static readonly string HARD_MODE = "HARD";
        public static readonly string NORMAL_MODE = "NORMAL";
    }

    [System.Serializable]
    public class GameState
    {
        private static Dictionary<string, GameStateInfo> STARTING_LEVEL_STATE = new Dictionary<string, GameStateInfo>
        {
        };
        private static GameState instance = null;
        public static GameState Instance
        {
            get
            {
                if (GameState.instance == null)
                {
                    GameState.instance = new GameState();
                }
                return GameState.instance;
            }
        }

        public int LOGINS = 0;
        public string LAST_LEVEL = null;
        public string CURRENT_LEVEL = "InsideGasStation";
        public float TOTAL_PLAY_TIME = 0f;
        public DateTime LAST_SAVE = DateTime.Now;
        public string CAMPAIGN_SELECTED = "UNKNOWN";
        public Dictionary<string,GameStateInfo> LEVEL_STATE = new Dictionary<string, GameStateInfo>(STARTING_LEVEL_STATE);
        public string GAME_MODE_SELECTED = GameModes.NORMAL_MODE;

        public override string ToString()
        {
            return $@"
            Game State:
                Logins: {this.LOGINS},
                Last level: {this.LAST_LEVEL},
                Current Level: {this.CURRENT_LEVEL},
                Total play time: {this.TOTAL_PLAY_TIME},
                Last Save: {this.LAST_SAVE},
                level state: {this.LEVEL_STATE},
                Campaign: {this.CAMPAIGN_SELECTED}
            ";
        }

        public static void SetCampaign(string campaign)
        {
            GameState.Instance.CAMPAIGN_SELECTED = campaign;
        }

        public static void SetGameState(GameState nGameState)
        {
            GameState.instance = nGameState;
        }

    }
}
