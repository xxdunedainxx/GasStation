using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using gasstation.code.core.data;
using gasstation.code.gameplay.levels;
using JsonKnownTypes;
using Newtonsoft.Json;

namespace gasstation.code.core.level
{
    [System.Serializable]
    [JsonConverter(typeof(JsonKnownTypesConverter<LevelStatePersistence>))]
    public class LevelStatePersistence
    {
        [JsonProperty("levelName")]
        public string levelName;

        public void Save()
        {
            Persistence.SaveLevelState(this.levelName, this);
        }

        public LevelStatePersistence(string name)
        {
            this.levelName = name;
        }
    }
}
