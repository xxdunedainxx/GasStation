using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using gasstation.code.core.objects;

namespace gasstation.code.core.level
{
    public class LevelTransitionHandler : SingletonMonobehaviour
    {
        [SerializeField]
        Canvas transitionCanvas;
        [SerializeField]
        Animator transitionBackgroundAnimator;
        [SerializeField]
        Image backgroundImage;

        public static LevelTransitionHandler GetHandler()
        {
            return (LevelTransitionHandler)LevelTransitionHandler.getInstance("LevelTransitionHandler");
        }

        public override void AfterStart()
        {
            this.transitionCanvas.enabled = true;
        }

        public void StartLevel()
        {
            this.backgroundImage.enabled = true;
            this.transitionBackgroundAnimator.Play("EnterLevel");
        }

        public void ExitLevel()
        {
            this.backgroundImage.enabled = true;
            this.transitionBackgroundAnimator.Play("ExitLevel");
        }

    }
}