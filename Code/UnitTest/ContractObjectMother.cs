namespace EnergyTrading.MDM.Test
{
    using System;

    using RWEST.Nexus.MDM.Contracts;

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
                case "ProductType":
                    return new ProductType
                        {
                            Details = Create<ProductTypeDetails>()
                        };

                case "ProductTypeDetails":
                    return new ProductTypeDetails
                        {
                            Name = "Name" + Guid.NewGuid(),
                            ShortName = "ShortName" + Guid.NewGuid()
                        };

                default:
                    throw new NotImplementedException("No OM for " + name);
            }
        }
    }
}
