using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using gasstation.code.core.level;
using JsonKnownTypes;
using Newtonsoft.Json;

namespace gasstation.code.gameplay.levels
{
    [System.Serializable]
    [JsonConverter(typeof(JsonKnownTypesConverter<BeginningOfGamePersistedState>))]
    class BeginningOfGamePersistedState : LevelStatePersistence
    {
        [JsonProperty("BearTrainingCompleted")]
        public bool BearTrainingCompleted = false;
        [JsonProperty("CleaningTrainingCompleted")]
        public bool CleaningTrainingCompleted = false;

        public BeginningOfGamePersistedState() : base(BeginningOfGame.BEGINNING_OF_GAME)
        {
        }
    }
}
