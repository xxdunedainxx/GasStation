using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gasstation.code.core.clicking
{
    public interface IClickable
    {
        void click();
        bool CanInteract();
        bool ClickEnabled();
    }
}