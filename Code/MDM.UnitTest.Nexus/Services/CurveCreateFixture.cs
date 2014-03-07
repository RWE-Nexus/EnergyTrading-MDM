namespace RWEST.Nexus.MDM.Test.Services
{
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using RWEST.Nexus.Data;
    using RWEST.Nexus.Mapping;
    using RWEST.Nexus.Search;
    using RWEST.Nexus.Validation;
    using RWEST.Nexus.MDM.Services;

    [TestClass]
    public class CurveCreateFixture
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

            var service = new CurveService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

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

            var service = new CurveService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var contract = new MDM.Contracts.Curve();

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

            var service = new CurveService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var curve = new Curve();
            var contract = new MDM.Contracts.Curve();

            validatorFactory.Setup(x => x.IsValid(It.IsAny<MDM.Contracts.Curve>(), It.IsAny<IList<IRule>>())).Returns(true);
            mappingEngine.Setup(x => x.Map<MDM.Contracts.Curve, Curve>(contract)).Returns(curve);

            // Act
            var expected = service.Create(contract);

            // Assert
            Assert.AreSame(expected, curve, "Curve differs");
            repository.Verify(x => x.Add(curve));
            repository.Verify(x => x.Flush());
        }
    }
}
	