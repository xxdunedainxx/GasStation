using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gasstation.code.core.preferences
{
    [Serializable]
    public class Preferences
    {

        public static KeyCode StringToKeyCode(string key)
        {
            return ((KeyCode)Enum.Parse(typeof(KeyCode), key.ToUpper()));
        }

        public KeyCode moveDown = Preferences.StringToKeyCode("S");
        public KeyCode moveUp = Preferences.StringToKeyCode("W");
        public KeyCode moveLeft = Preferences.StringToKeyCode("A");
        public KeyCode moveRight = Preferences.StringToKeyCode("D");
        public KeyCode inventory = Preferences.StringToKeyCode("I");
    }
}