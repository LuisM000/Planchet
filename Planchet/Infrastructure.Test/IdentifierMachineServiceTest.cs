using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure.Services;
using Test.Base.Attributes;
using Test.Base.Enums;

namespace Infrastructure.Test
{
    [TestClass]
    public class IdentifierMachineServiceTest
    {
        /// <summary>
        /// Test that two instances of identifier returns the same identifier
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.Infrastructure)]
        public void UniqueIdentifierTest()
        {
            IdentifierMachineService identifierMachineService1 = new IdentifierMachineService();
            IdentifierMachineService identifierMachineService2 = new IdentifierMachineService();

            string identifier1 = identifierMachineService1.GetUniqueIdentifier();
            string identifier2 = identifierMachineService2.GetUniqueIdentifier();

            Assert.IsTrue(!String.IsNullOrEmpty(identifier1));
            Assert.AreEqual(identifier1, identifier2);
        }
    }
}
