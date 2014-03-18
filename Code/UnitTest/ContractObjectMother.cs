namespace EnergyTrading.MDM.Test
{
    using System;

    public class ContractObjectMother
    {
        public static T Create<T>()
        {
            var value = Create(typeof(T).Name);

            return (T)value;
        }

        public static object Create(string name)
        {
            switch (name)
            {
                default:
                    throw new NotImplementedException("No OM for " + name);
            }
        }
    }
}
