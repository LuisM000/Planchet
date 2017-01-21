using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using Moq;
using Infrastructure.IServices;

namespace Databases.Factories.Test
{
    [TestClass]
    public class DatabaseFactoryMachineSQLTest
    {
        [TestMethod]
        public void IdentifierInDatabaseNameTest()
        {
            Mock<IDatabaseInitializer<DataBaseSQL>> initializerMoq = new Mock<IDatabaseInitializer<DataBaseSQL>>();
            Mock<IIdentifierMachineService> identifierMachineServiceMoq=new Mock<IIdentifierMachineService>();

            identifierMachineServiceMoq.Setup(i=>i.GetUniqueIdentifier()).Returns("identifier");
            FactoryMachineSQL factoryUniqueSQL = new FactoryMachineSQL(initializerMoq.Object, identifierMachineServiceMoq.Object);

            DataBase database = factoryUniqueSQL.CreateDataBase("SampleDatabase");
            Assert.AreEqual("SampleDatabase-identifier", database.Database.Connection.Database);
        }

        [TestMethod]
        public void IdentifierInDatabaseNameConfigTest()
        {
            Mock<IDatabaseInitializer<DataBaseSQL>> initializerMoq = new Mock<IDatabaseInitializer<DataBaseSQL>>();
            Mock<IIdentifierMachineService> identifierMachineServiceMoq = new Mock<IIdentifierMachineService>();

            identifierMachineServiceMoq.Setup(i => i.GetUniqueIdentifier()).Returns("identifier");
            FactoryMachineSQL factoryUniqueSQL = new FactoryMachineSQL(initializerMoq.Object, identifierMachineServiceMoq.Object);

            DataBase database = factoryUniqueSQL.CreateDataBase(String.Empty);
            Assert.AreEqual("Planchet-identifier", database.Database.Connection.Database);
        }
    }
}
