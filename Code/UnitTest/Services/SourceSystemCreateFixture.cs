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
    public class SourceSystemCreateFixture
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

            var service = new SourceSystemService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

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

            var service = new SourceSystemService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var contract = new RWEST.Nexus.MDM.Contracts.SourceSystem();

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

            var service = new SourceSystemService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var sourcesystem = new SourceSystem();
            var contract = new RWEST.Nexus.MDM.Contracts.SourceSystem();

            validatorFactory.Setup(x => x.IsValid(It.IsAny<RWEST.Nexus.MDM.Contracts.SourceSystem>(), It.IsAny<IList<IRule>>())).Returns(true);
            mappingEngine.Setup(x => x.Map<RWEST.Nexus.MDM.Contracts.SourceSystem, SourceSystem>(contract)).Returns(sourcesystem);

            // Act
            var expected = service.Create(contract);

            // Assert
            Assert.AreSame(expected, sourcesystem, "SourceSystem differs");
            repository.Verify(x => x.Add(sourcesystem));
            repository.Verify(x => x.Flush());
        }
    }
}
	