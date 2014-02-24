namespace EnergyTrading.MDM.Test.Services
{
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;
    using EnergyTrading.MDM.Services;

    [TestClass]
    public class LocationRoleCreateFixture
    {
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void NullContractInvalid()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>(); 
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
			var searchCache = new Mock<ISearchCache>();

            var service = new LocationRoleService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            validatorFactory.Setup(x => x.IsValid(It.IsAny<object>(), It.IsAny<IList<IRule>>())).Returns(false);
           
            // Act
            service.Create(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void InvalidContractNotSaved()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
			var searchCache = new Mock<ISearchCache>();

            var service = new LocationRoleService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var contract = new RWEST.Nexus.MDM.Contracts.LocationRole();

            validatorFactory.Setup(x => x.IsValid(It.IsAny<object>(), It.IsAny<IList<IRule>>())).Returns(false);

            // Act
            service.Create(contract);
        }

        [TestMethod]
        public void ValidContractIsSaved()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
			var searchCache = new Mock<ISearchCache>();

            var service = new LocationRoleService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var locationrole = new LocationRole();
            var contract = new RWEST.Nexus.MDM.Contracts.LocationRole();

            validatorFactory.Setup(x => x.IsValid(It.IsAny<RWEST.Nexus.MDM.Contracts.LocationRole>(), It.IsAny<IList<IRule>>())).Returns(true);
            mappingEngine.Setup(x => x.Map<RWEST.Nexus.MDM.Contracts.LocationRole, LocationRole>(contract)).Returns(locationrole);

            // Act
            var expected = service.Create(contract);

            // Assert
            Assert.AreSame(expected, locationrole, "LocationRole differs");
            repository.Verify(x => x.Add(locationrole));
            repository.Verify(x => x.Flush());
        }
    }
}
	