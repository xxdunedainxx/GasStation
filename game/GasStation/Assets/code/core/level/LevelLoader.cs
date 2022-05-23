using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using gasstation.code.core.objects;
using gasstation.code.core.data;

namespace gasstation.code.core.level
{
    public class LevelLoader : SingletonMonobehaviour
    {
        public override void AfterStart() { }

        public static LevelLoader GetLoader()
        {
            return (LevelLoader)LevelLoader.getInstance("LevelLoader");
        }

        public void Transition(string level)
        {
            GameState.Instance.LAST_LEVEL = GameState.Instance.CURRENT_LEVEL;
            Persistence.Save();
            LevelTransitionHandler.GetHandler().ExitLevel();
            SceneManager.LoadScene(level);
        }
    }
}
