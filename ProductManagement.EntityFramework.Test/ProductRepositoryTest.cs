using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProductManagement.EntityFramework.Test
{
    [TestClass]
    public class ProductRepositoryTest
    {
        private DatabaseEntities context;
        private Guid globalID;

        [TestInitialize]
        public void TestInit()
        {
            context = new DatabaseEntities();
            globalID = Guid.Parse("481bc46b-a68d-413f-abac-030baf300d93");
        }

        [TestMethod]
        public void Product_DoesNotExist()
        {
            ProductRepository target = new ProductRepository(context);
            var test = target.Find(Guid.NewGuid());
            Assert.IsNull(test);
        }

        [TestMethod]
        public void Product_Repository_Test()
        {
            ProductRepository target = new ProductRepository(context);
            var test = target.Add(GetTestObject());
            context.SaveChanges();
            Assert.IsNotNull(test);

            var testFind = target.Find(globalID);
            Assert.IsNotNull(testFind);

            target.Remove(testFind);
            context.SaveChanges();
        }       

        [TestCleanup()]
        public void ClassCleanup()
        {
            ProductRepository target = new ProductRepository(context);
            var test = target.Find(globalID);
            if (test != null)
            {
                target.Remove(test);
            }            
        }

        private Product GetTestObject()
        {
            return new Product
            {
                Id = globalID,
                Description = "Test Description",
                Name ="Test Name"
            };
        }
    }
}
