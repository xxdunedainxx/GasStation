using System;
using System.Collections.Generic;

using gasstation.code.gameplay.levels;


namespace gasstation.code.core.level
{
    class LevelFactory
    {
        public static string LOGIN = "Login";
        public static string INSIDE_GAS_STATION = "InsideGasStation";
        public static string INSIDE_GAS_STATION2 = "InsideGasStation2";

        static Dictionary<string, Func<Level>> LEVELS = new Dictionary<string, Func<Level>>
        {
            { LevelFactory.LOGIN, LevelFactory.GetLogin},
            { LevelFactory.INSIDE_GAS_STATION, LevelFactory.GetInsideGasStation },
            { LevelFactory.INSIDE_GAS_STATION2, LevelFactory.GetInsideGasStation }
        };

        public static Level FetchLevel(string name)
        {
            return LevelFactory.LEVELS[name]();
        }

        public static Login GetLogin()
        {
            return new Login();
        }

        public static InsideGasStation GetInsideGasStation()
        {
            return new InsideGasStation();
        }

    }
}
