namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class FeeTypeMapper : Mapper<EnergyTrading.MDM.FeeType, OpenNexus.MDM.Contracts.FeeType>
    {
        public override void Map(EnergyTrading.MDM.FeeType source, OpenNexus.MDM.Contracts.FeeType destination)
        {
			// TODO_CodeGeneration - this needs to be completed if we need to map any top level entity attributes. An example can be found on PartyRole
        }
    }
}
