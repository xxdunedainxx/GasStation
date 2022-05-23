using gasstation.code.core.level;
using gasstation.code.core.level;
using gasstation.code.core.dialogue;
using gasstation.code.core.data;
using gasstation.code.core.logging;

using UnityEngine;
using UnityEngine.UI;

namespace gasstation.code.gameplay.levels
{
    public class LoginButton : MonoBehaviour
    {
        [SerializeField]
        Canvas LoadScreenCanvas;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        public void StartClick()
        {
            LogFactory.INFO("Lets get started clicked...");
            LoadScreenCanvas.gameObject.SetActive(true);
        }
    }
}