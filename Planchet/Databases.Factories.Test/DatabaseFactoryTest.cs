using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using Model.Entities.Image;
using Test.Base.Attributes;
using Test.Base.Enums;

namespace Databases.Factories.Test
{
    [TestClass]
    public class DatabaseFactoryTest
    { 
        /// <summary>
        /// Test that creates DB if not exists
        /// </summary>
        [TestTraits(Trait.Database)]
        [TestCategory("NotBuild")]
        [TestMethod]
        public void NewDBCreationTest()
        {
            IDatabaseInitializer<DataBaseSQL> initializer = new DropCreateDatabaseAlways<DataBaseSQL>();
            FactorySQL factory = new FactorySQL(initializer);

            DataBase db = factory.CreateDataBase("PlanchetDummy");
            db.Set<Screenshot>().Add(new Screenshot() { Id=0});
            db.SaveChanges();
            db.Dispose();
        }
    }
}
