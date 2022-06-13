using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using gasstation.code.core.logging;
using gasstation.code.core.player;
using gasstation.code.core.level;
using gasstation.code.gameplay.levels;
using gasstation.code.core.ui.intereactable;
using JsonKnownTypes;

namespace gasstation.code.core.data
{
    [Serializable]
    public class SaveState
    {
        [JsonProperty]
        public GameState gameState;

        [JsonProperty]
        public PlayerState playerState;

        [JsonProperty]
        public LevelStates levelStates;

        [JsonProperty]
        public CashRegisterState registerState;

        public SaveState(bool firstTime)
        {
            this.gameState = new GameState();
            this.playerState = new PlayerState();
            this.levelStates = new LevelStates();
            this.registerState = new CashRegisterState();
        }

        public void UpdateGameState()
        {
            this.gameState = GameState.Instance;
        }

        public ref CashRegisterState GetRegisterState()
        {
            return ref this.registerState;
        }

        public void UpdatePlayerState()
        {
            PlayerState.Instance.UpdatePlayerState();
            this.playerState = PlayerState.Instance;
        }

        public void UpdateLevelStates()
        {

        }

        public void UpdateLevelState(string levelState, LevelStatePersistence nLevelState)
        {
            this.levelStates.levelStates[levelState] = nLevelState;
        }

        public void Update()
        {
            this.UpdateGameState();
            this.UpdatePlayerState();
            this.UpdateLevelStates();
        }
    }

    public class Persistence
    {
        private static string persistencePath = "game_data.json";
        private static string memSlotFilePath = "MEM_SLOT.txt";
        public static PersistenceData dataObject;
        public static bool isReady  = false;

        public static void NewGame(int memorySlot)
        {
            Persistence.dataObject.saveStates[memorySlot] = new SaveState(true);
            Persistence.WriteData();
        }

        public static void LoadSaveState(int memorySlot)
        {
            Persistence.dataObject.currentSaveState = Persistence.dataObject.saveStates[memorySlot];
        }

        public static void CheckSaveSlot()
        {
            if (File.Exists(Persistence.memSlotFilePath))
            {
                int memSlot = Int32.Parse(File.ReadAllText(Persistence.memSlotFilePath));
                Persistence.LoadSaveState(memSlot);
            }
        }

        public static void WriteMemorySlotFile(int memorySlot)
        {
            Persistence.WipeMemorySlotFile();
            File.WriteAllText(Persistence.memSlotFilePath, $"{memorySlot}");
        }

        public static void WipeMemorySlotFile()
        {
            if (File.Exists(Persistence.memSlotFilePath))
            {
                File.Delete(Persistence.memSlotFilePath);
            }
        }

        public static bool IsReady()
        {
            return Persistence.isReady;
        }

        public static void Ready()
        {
            Persistence.isReady = true;
        }

        public static void Init()
        {
            if (!File.Exists(persistencePath))
            {
                Persistence.FirstSave();
            }
            else
            {
                Persistence.ReadData();
            }
            Persistence.Ready();
        }

        private static void FirstSave()
        {
            Persistence.dataObject = new PersistenceData();
            // Default 3 save slots
            Persistence.dataObject.saveStates = new SaveState[3];
            Persistence.WriteData();
        }

        public static void SaveLevelState(string levelState, LevelStatePersistence nLevelState)
        {
            Persistence.dataObject.UpdateLevelState(levelState, nLevelState);
            Persistence.Save();
        }

        public static LevelStatePersistence FetchLevelStatePersistence(string levelState)
        {
            return Persistence.dataObject.FetchLevelStatePersistence(levelState);
        }

        public static void Save()
        {
            Persistence.dataObject.Update();
            Persistence.WriteData();
        }

        public static void WriteData()
        {
            if (File.Exists(Persistence.persistencePath))
            {
                File.Delete(Persistence.persistencePath);
            }
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(Persistence.dataObject, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(Persistence.persistencePath, output);
        }

        public static ref CashRegisterState FetchRegisterState()
        {
            return ref Persistence.dataObject.currentSaveState.GetRegisterState();
        }

        public static void ReadData()
        {
            Persistence.dataObject = null;
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None };
            JsonSerializer serializer = JsonSerializer.Create(settings);
            using (StreamReader sr = new StreamReader(Persistence.persistencePath))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                Persistence.dataObject = serializer.Deserialize<PersistenceData>(reader);
            }
            Persistence.CheckSaveSlot();
            Persistence.UpdateInMemoryObjects();
        }

        public static void UpdateInMemoryObjects()
        {
            Persistence.UpdatePlayerInventory();
            Persistence.UpdateGameState();
        }

        public static void UpdatePlayerInventory()
        {
            if (Persistence.dataObject.currentSaveState != null &&
                Persistence.dataObject.currentSaveState.playerState.playerInventory != null
            )
            {
                Player.GetPlayer().SetPlayerInventory(Persistence.dataObject.currentSaveState.playerState.playerInventory);
            }
        }

        public static void UpdateGameState()
        {
            if (Persistence.dataObject.currentSaveState != null)
            {
                GameState.SetGameState(Persistence.dataObject.currentSaveState.gameState);
            }
        }

        [Serializable]
        [JsonKnownThisType("BeginningOfGamePersistedState")]
        public class PersistenceData
        {
            public string exampleData = "something";
            public SaveState currentSaveState = null;

            [JsonProperty]
            public SaveState[] saveStates;

            public void Update()
            {
                this.UpdateSaveState();
            }

            public void UpdateSaveState()
            {
                if (this.currentSaveState != null)
                {
                    this.currentSaveState.Update();
                }
            }

            public void UpdateLevelState(string levelState, LevelStatePersistence nLevelState)
            {
                this.currentSaveState.UpdateLevelState(levelState, nLevelState);
            }

            public LevelStatePersistence FetchLevelStatePersistence(string levelState)
            {
                return this.currentSaveState.levelStates.levelStates[levelState];
            }
        }
    }
}
