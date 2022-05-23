using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using UnityEngine;

namespace gasstation.code.core.constants
{
    class Sprites
    {
        public static string defaultItemSpriteName = "images/items/grey-question-mark";
        public static string floorSpill1 = "images/objects/FloorSpill1";
        public static string floorSpill2 = "images/objects/FloorSpill2";

        public static Sprite defaultItemSprite = Resources.Load(Sprites.defaultItemSpriteName, typeof(Sprite)) as Sprite;
        public static Sprite floorSpill1Sprite = Resources.Load(Sprites.floorSpill1, typeof(Sprite)) as Sprite;
        public static Sprite floorSpill2Sprite = Resources.Load(Sprites.floorSpill2, typeof(Sprite)) as Sprite;

        private static Dictionary<string, Sprite> spriteLookupTable = new Dictionary<string, Sprite>
        {
            {Sprites.defaultItemSpriteName, Sprites.defaultItemSprite },
            {Sprites.floorSpill1, Sprites.floorSpill1Sprite },
            {Sprites.floorSpill2, Sprites.floorSpill2Sprite }
        };


        public static Sprite LookupSprite(string name)
        {
            if (!(spriteLookupTable.ContainsKey(name)))
            {
                return Sprites.spriteLookupTable[Sprites.defaultItemSpriteName];
            } else
            {
                return Sprites.spriteLookupTable[name];
            }
        }
    }
}
