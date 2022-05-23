using gasstation.code.core.level;
using gasstation.code.core.data;
using gasstation.code.core.logging;
using gasstation.code.core.dialogue;
using gasstation.code.core.npc;

using UnityEngine;

using System.Collections.Generic;
using System;

namespace gasstation.code.gameplay.levels
{
    public class InsideGasStation : Level
    {
        public static SpriteRenderer hallwayBackground;
        public InsideGasStation() : base(LevelFactory.INSIDE_GAS_STATION)
        {

        }

        public override void SetupLevelState()
        {
            this.GetLevelState(LevelState.BEGINNING_OF_GAME); 
            base.SetupLevelState();
            this.levelState.ApplyLevelState();
        }

        public override void StartLevel()
        {
            this.levelStateMap = new Dictionary<string, Func<LevelState>>
            {
                {LevelState.BEGINNING_OF_GAME,  BeginningOfGame.InitBeginningOfGameState}
            };

            base.StartLevel();
            this.levelState.LevelStateStart();
        }

    }
}
