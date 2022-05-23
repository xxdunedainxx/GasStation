using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using gasstation.code.core.data;
using gasstation.code.core.objects;
using gasstation.code.core.logging;
using gasstation.code.core.level;

public class SaveStateManager : GasStationChild
{
    [SerializeField]
    public int saveSlot;

    [SerializeField]
    public Text saveStateText;

    SaveState boundSaveState;

    public void ClickSaveState()
    {
        if (this.boundSaveState == null)
        {
            LogFactory.INFO("Start new game!");
            Persistence.NewGame(this.saveSlot);
            this.boundSaveState = Persistence.dataObject.saveStates[this.saveSlot];
        }
        LogFactory.INFO("Load game!");
        Persistence.LoadSaveState(this.saveSlot);
        Persistence.WriteMemorySlotFile(this.saveSlot);
        LevelLoader.GetLoader().Transition(this.boundSaveState.gameState.CURRENT_LEVEL);
    }

    public override void AfterStart()
    {
        this.boundSaveState = Persistence.dataObject.saveStates[this.saveSlot];
        if (this.boundSaveState == null)
        {
            this.saveStateText.text = "* New Game";
        }
    }
}
