namespace EnergyTrading.MDM.Test
{
    using EnergyTrading.Test;

    public class Fixture : EnergyTrading.Test.Fixture
    {
        protected override ICheckerFactory CreateCheckerFactory()
        {
            return new CheckerFactory();
        }
    }
}