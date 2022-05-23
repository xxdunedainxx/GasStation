using UnityEngine;
using gasstation.code.core.logging;
using gasstation.code.core.data;

namespace gasstation.code.core.setup
{
    public class Setup
    {
        public static void CoreSetup()
        {
            LogFactory.Init();
            LogFactory.INFO("====== EXECUTING SETUP ======");
            SetupData.SetupPersistence();
        }
    }

    class SetupData
    {
        public static void SetupPersistence()
        {
            LogFactory.INFO("setting up persistence..");
            Persistence.Init();
            Version.Init();
        }
    }
}
