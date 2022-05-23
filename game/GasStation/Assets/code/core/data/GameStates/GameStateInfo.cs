using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gasstation.code.core.data
{
    [System.Serializable]
    public class GameStateInfo
    {
        public string name;

        public GameStateInfo(string name)
        {
            this.name = name;
        }
    }
}
