namespace RWEST.Nexus.MDM.Contracts
{
    public class VersionedMdmEntity
    {
        public string EntityVersion { get; set; }
        public IMdmEntity Entity { get; set; }

        public T As<T>() where T : class, IMdmEntity
        {
            return Entity as T;
        }
    }
}