using System.Collections.Generic;
using gasstation.code.core.logging;
using gasstation.code.core.player;
using gasstation.code.core.data;
using gasstation.code.core.objects;


using UnityEngine;
using System.Collections;

namespace gasstation.code.core.level
{
    public class LevelState : SingletonMonobehaviour
    {
        /// <summary>
        /// Intended for override by child LevelState
        /// </summary>
        /// 
        public readonly static string BEGINNING_OF_GAME= "BeginningOfGame";
        public static string Day = "06/06/82"; // default start date beginning of game

        public virtual void ApplyLevelState() { }
        public virtual void LevelStateStart() { }
        public virtual void AttachToMainGame() { }
        public override void AfterStart(){}
    }
}
