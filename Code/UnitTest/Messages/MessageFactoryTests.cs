namespace EnergyTrading.MDM.Test.Messages
{
    using System;
    using System.Collections.Specialized;

    using NUnit.Framework;

    using EnergyTrading.MDM.Messages;

    [TestFixture]
    public class MessageFactoryTests
    {
        private const string DateFormatString = "yyyy-MM-dd'T'HH:mm:ss.fffffffZ";

        [Test]
        public void MessageFactoryParseTest()
        {
            DateTime dateBeforeParse = new DateTime(2011, 1, 1, 0, 0, 0);
            var dateAsString = dateBeforeParse.ToString(DateFormatString);
            var nameValues = new NameValueCollection();
            nameValues.Add("as-of", dateAsString);

            var request = MessageFactory.GetRequest(nameValues);

            Assert.AreEqual(request.ValidAt, dateBeforeParse);
        }
    }
}
