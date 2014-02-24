namespace EnergyTrading.MDM.Test
{
    using EnergyTrading.Test;

    public class CheckerFactory : EnergyTrading.Test.CheckerFactory
    {
        public CheckerFactory()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            this.Builder = new CheckerBuilder(this);

            this.Register(typeof(CheckerFactory).Assembly);
        }
    }
}
