using gasstation.code.core.level;
using gasstation.code.core.data;
using gasstation.code.core.logging;

using UnityEngine;

namespace gasstation.code.gameplay.levels
{
    public class Login : Level
    {
        public static SpriteRenderer hallwayBackground;
        public Login() : base(LevelFactory.LOGIN)
        {

        }

        public override void StartLevel()
        {
            // Wipe the memory slot, as it must be loaded by this process
            Persistence.WipeMemorySlotFile();
            base.StartLevel();
        }

    }
}
