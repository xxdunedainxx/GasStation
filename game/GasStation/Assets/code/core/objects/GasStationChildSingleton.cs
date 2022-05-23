using UnityEngine;
using System.Collections;

namespace gasstation.code.core.objects
{
    public abstract class GasStationChildSingleton : SingletonMonobehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(this.WaitForGame());
        }

        public override void AfterStart()
        {

        }

        public abstract void AfterGasStationStart();

        bool IsGameReady()
        {
            return ((GasStation)GasStation.getInstance("GasStation")).isReady;
        }

        IEnumerator WaitForGame()
        {
            yield return new WaitUntil(this.IsGameReady);
            this.AfterGasStationStart();
        }
    }
}
