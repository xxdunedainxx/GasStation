using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gasstation.code.core.ui.intereactable
{
    public class Cleaning : MonoBehaviour
    {
        public static bool CleaningRequired()
        {
            GameObject[] spills = GameObject.FindGameObjectsWithTag("Spill");

            if (spills.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}