using System;
using System.Collections.Generic;
using Newtonsoft.Json;

using gasstation.code.core.level;
using gasstation.code.gameplay.levels;

namespace gasstation.code.core.data
{
    [System.Serializable]
    public class LevelStates
    {
        [JsonProperty("levelStates")]
        public Dictionary<string, LevelStatePersistence> levelStates = new Dictionary<string, LevelStatePersistence>
        {
            {BeginningOfGame.BEGINNING_OF_GAME, new BeginningOfGamePersistedState()}
        };
    }
}
