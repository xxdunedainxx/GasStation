using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace gasstation.code.core.ui.intereactable
{
    [System.Serializable]
    public class CashRegisterState
    {
        [JsonProperty("totalCash")]
        public float totalCash = 100f;

        [JsonProperty("deficitCash")]
        public float deficitCash = 0;

        [JsonProperty("cashStolen")]
        public float cashStolen = 0;

        [JsonProperty("bullets")]
        public int bullets = 0;
    }
}
