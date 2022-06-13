using gasstation.code.core.data;

namespace gasstation.code.core.ui.intereactable.register
{
    public static class CashRegisterSystem
    {
        /* Class abstractions for updating CashRegister values stored in CashRegister State, values are applied (negative or positive)
         * 
         * 
         */ 

        public static float EnsureValueZero(float value)
        {
            if(value >= 0)
            {
                return value;
            } else
            {
                return 0;
            }
        }

        public static void UpdateTotalCashValue(float valueToApply)
        {
            ref CashRegisterState register = ref Persistence.dataObject.currentSaveState.GetRegisterState();
            register.totalCash = CashRegisterSystem.EnsureValueZero(valueToApply + register.totalCash);
        }

        public static void UpdateDeficit(float valueToApply)
        {
            ref CashRegisterState register = ref Persistence.dataObject.currentSaveState.GetRegisterState();
            register.deficitCash = CashRegisterSystem.EnsureValueZero(valueToApply + register.deficitCash);
        }

        public static void UpdateCashStolen(float valueToApply)
        {
            ref CashRegisterState register = ref Persistence.dataObject.currentSaveState.GetRegisterState();
            register.cashStolen = CashRegisterSystem.EnsureValueZero(valueToApply + register.cashStolen);
        }



    }
}
