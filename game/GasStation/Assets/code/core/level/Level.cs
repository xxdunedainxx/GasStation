using System.Collections.Generic;
using gasstation.code.core.logging;
using gasstation.code.core.player;
using gasstation.code.core.data;

using UnityEngine;
using System.Collections;
using System;

namespace gasstation.code.core.level
{
    public class Level : MonoBehaviour
    {
        public string name;
        public bool isVerticalLevel;
        public bool requiresDialogue;
        protected Dictionary<string, Func<LevelState>> levelStateMap;
        protected Dictionary<string, Vector3> transitionPosition = null;
        protected LevelState levelState = null;


        public Level(string name, bool isVerticalLevel = false, bool dialogue = true)
        {
            LogFactory.INFO($"'{name}' level Instantiated");

            this.name = name;
            this.isVerticalLevel = isVerticalLevel;
            this.requiresDialogue = dialogue;
        }

        protected void GetLevelState(string levelStateToSet)
        {
            this.levelState = this.levelStateMap[levelStateToSet]();
        }

        public virtual void SetupLevelState()
        {

        }

        public virtual void prepareLevel()
        {
            LogFactory.INFO("{rep level state and transition handler");
            this.SetupLevelState();
            this.transitionHandler();
        }

        public virtual void constructEventTree()
        {
            LogFactory.INFO("Level construct event tree called");
        }

        public virtual void StartLevel()
        {
            this.prepareLevel();
            LogFactory.INFO("empty start level..");
            GameState.Instance.CURRENT_LEVEL = this.name;
            if (isVerticalLevel)
            {
                LogFactory.INFO("applying vertical graviy");
                this.applyVerticalGravity();
            }

            Persistence.Save();
            //Persistence.dataObject.PrintData();

            if (this.transitionPosition != null)
            {
                this.AdjustPlayerPosition();
            }

            this.levelState.AttachToMainGame();
        }

        private void AdjustPlayerPosition()
        {
            if (GameState.Instance.LAST_LEVEL != null && this.transitionPosition.ContainsKey(GameState.Instance.LAST_LEVEL))
            {
                Player.GetPlayer().transform.position = this.transitionPosition[GameState.Instance.LAST_LEVEL];
            }
        }

        protected bool CheckDependencies()
        {
            return (
                Player.IsPlayerReady()
            );
        }

        public virtual IEnumerator StartLevelAndWaitForDependencies()
        {
            yield return new WaitUntil(this.CheckDependencies);
            this.StartLevel();
        }

        public static void levelTransition(string toLevel)
        {
            // LevelLoader.instance.levelTransition(toLevel);
        }

        private void applyVerticalGravity()
        {
            /*player p = GameState.getGameState().playerReference;
            p.gameObject.GetComponent<Rigidbody2D>().gravityScale = 5;
            p.getPlayerState().movementSpeed = 5;
            p.sideScrolling = true;*/
        }

        public static void DisableLevelObjects(List<string> objects)
        {
            foreach (string obj in objects)
            {
                GameObject.Find(obj).SetActive(false);
            }
        }

        public virtual void transitionHandler()
        {
            /*if (this.transitionHandlers != null && this.transitionHandlers.ContainsKey(GameState.getGameState().gsStore.LAST_LEVEL))
            {
                //GameState.getGameState().playerReference.adjustPlayerPosition(this.transitionHandlers[GameState.getGameState().gsStore.LAST_LEVEL]);
            }*/
        }

    }
}
