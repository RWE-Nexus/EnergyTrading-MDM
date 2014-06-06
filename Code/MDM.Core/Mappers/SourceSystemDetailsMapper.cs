namespace EnergyTrading.Mdm.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.Mdm.Extensions;

    public class SourceSystemDetailsMapper : Mapper<EnergyTrading.Mdm.SourceSystem, EnergyTrading.Mdm.Contracts.SourceSystemDetails>
    {
        public override void Map(EnergyTrading.Mdm.SourceSystem source, EnergyTrading.Mdm.Contracts.SourceSystemDetails destination)
        {
            destination.Name = source.Name;
            destination.Parent = source.Parent.CreateNexusEntityId(() => source.Parent.Name);
        }
    }
}