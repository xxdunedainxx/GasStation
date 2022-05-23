using UnityEngine;
using System.Collections;
using gasstation.code.core.logging;
using gasstation.code.core.setup;
using gasstation.code.core.objects;
using gasstation.code.core.level;
using gasstation.code.core.dialogue;
using gasstation.code.core.data;
using System;
using System.IO;
using UnityEngine.EventSystems;

namespace gasstation
{
    public class GasStation : SingletonMonobehaviour
    {
        [SerializeField]
        string level;
        Level lvl;
        [SerializeField]
        public SpriteRenderer lvlBackground = null;
        [SerializeField]
        public EventSystem eventSystem;
        public static float lvlWidth = 0;
        public static float lvlHeight = 0;

        public bool isReady = false;

        public static EventSystem GetEventSystem()
        {
            return GasStation.GetGame().eventSystem;
        }

        public static GasStation GetGame()
        {
            return (GasStation)GasStation.getInstance("GasStation");
        }

        // Start is called before the first frame update
        public void Start()
        {
            Setup.CoreSetup();
            SingletonMonobehaviour.PrintSingletons();
            LevelTransitionHandler.GetHandler().StartLevel();
            this.GetLevelDimnensions();
            this.GetLevel();
            LogFactory.INFO("Game started!");
            /*string[] inputs = Input.GetJoystickNames();
            foreach(string input in inputs)
            {
                LogFactory.INFO($"joycon inputs {input}");
            }*/
            StartCoroutine(this.WaitForDependencies());
        }

        private void GetLevelDimnensions()
        {
            if (this.lvlBackground != null)
            {
                GasStation.lvlHeight = this.lvlBackground.bounds.size.y;
                GasStation.lvlWidth = this.lvlBackground.bounds.size.x;
            }
        }

        private void GetLevel()
        {
            Debug.unityLogger.Log($"GasStation is getting level {level}");
            this.lvl = LevelFactory.FetchLevel(this.level);
        }

        public override void AfterStart()
        {

        }

        private bool CheckDependencies()
        {
            return (
                true
            );
        }

        private void AfterDependencies()
        {
            this.isReady = true;
            StartCoroutine(this.lvl.StartLevelAndWaitForDependencies());
        }

        IEnumerator WaitForDependencies()
        {
            yield return new WaitUntil(this.CheckDependencies);
            this.AfterDependencies();
        }
    }
}