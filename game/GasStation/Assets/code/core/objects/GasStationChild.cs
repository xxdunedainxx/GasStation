using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gasstation.code.core.objects
{
    public abstract class GasStationChild : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(this.WaitForTextBoxSet());
        }

        public abstract void AfterStart();

        bool IsGameReady()
        {
            return ((GasStation)GasStation.getInstance("GasStation"))?.isReady == true;
        }

        IEnumerator WaitForTextBoxSet()
        {
            yield return new WaitUntil(this.IsGameReady);
            this.AfterStart();
        }
    }
}