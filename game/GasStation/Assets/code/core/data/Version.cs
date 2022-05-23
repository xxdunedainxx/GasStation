using System.IO;
using gasstation.code.core.logging;

namespace gasstation.code.core.data
{
    class Version
    {
        private static readonly float VERSION = .01f;
        private static readonly string VERSION_FILE = "VERSION.txt";
        private static float CURRENT_VERSION = 0f;

        public static void Init()
        {
            LogFactory.INFO("Grabbing app version");
            if (!File.Exists(Version.VERSION_FILE))
            {
                LogFactory.INFO($"Version file does not exist, laying down version # {Version.VERSION}");
                File.WriteAllText(Version.VERSION_FILE, Version.VERSION.ToString());
                Version.CURRENT_VERSION = Version.VERSION;
            }
            else
            {
                Version.CURRENT_VERSION = float.Parse(File.ReadAllText(Version.VERSION_FILE));
            }
            LogFactory.INFO($"Last Version: {Version.CURRENT_VERSION}, vs Recent App Version {Version.VERSION}");

            if (Version.VersionChange())
            {
                LogFactory.INFO("WARNING, VERSIONS DO NOT MATCH");
            }
        }

        public static bool VersionChange()
        {
            return Version.VERSION != Version.CURRENT_VERSION;
        }
    }
}
