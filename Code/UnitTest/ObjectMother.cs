namespace EnergyTrading.MDM.Test
{
    using System;

    using EnergyTrading.Data;

    public static class ObjectMother
    {
        public static T Create<T>()
            where T : IIdentifiable
        {
            var value = Create(typeof(T).Name);

            return (T)value;
        }

        public static IIdentifiable Create(string name)
        {
            switch (name)
            {
                // TODO_UnitTestGeneration - Add new entity here

                case "SourceSystem":
                    return new SourceSystem
                    {
                        Name = "SourceSystem" + Guid.NewGuid()
                    };


                default:
                    throw new NotImplementedException("No OM for " + name);
            }
        }
    }
}