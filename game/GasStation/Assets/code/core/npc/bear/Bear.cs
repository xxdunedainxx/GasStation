namespace gasstation.code.core.npc.bear
{
    public class Bear : Npc
    {
        private bool __isBearAwake = true;

        public static Bear GetBear()
        {
            return (Bear)Bear.getInstance("Bear");
        }

        public static bool CheckIfBearIsAwake()
        {
            return Bear.GetBear().IsBearAwake();
        }

        public void WakeBearUp()
        {
            this.__isBearAwake = true;
        }

        public void BearSleep()
        {
            this.__isBearAwake = false;
        }

        public bool IsBearAwake()
        {
            return this.__isBearAwake;
        }
    }
}
