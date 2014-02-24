namespace EnergyTrading.MDM
{
    public partial class Person
    {
        partial void CopyDetails(PersonDetails details)
        {
            // force the load of related entities to make sure that updating these to null deletes the relationship in EF
            var forceLoadOfRole = this.LatestDetails.Role;

            this.LatestDetails.FirstName = details.FirstName;
            this.LatestDetails.LastName = details.LastName;
            this.LatestDetails.Phone = details.Phone;
            this.LatestDetails.Role = details.Role;
            this.LatestDetails.Email = details.Email;
            this.LatestDetails.Validity = this.LatestDetails.Validity.ChangeStart(details.Validity.Start).ChangeFinish(details.Validity.Finish);
            this.LatestDetails.Person = this;
        }
    }
}