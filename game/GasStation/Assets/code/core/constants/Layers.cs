using UnityEngine;

namespace gasstation.code.core.constants
{
    class Layers
    {
        public static LayerMask PLAYER_LAYER = LayerMask.GetMask("Player");
        public static int PLAYER_LAYER_VALUE = LayerMask.NameToLayer("Player");
        public static LayerMask GROUND_LAYER = LayerMask.GetMask("Ground");
    }
}