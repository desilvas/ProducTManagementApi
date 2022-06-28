using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductManagement.Data;
using ProductManagement.Domain.Service;
using ProductManagement.EntityFramework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class ProductServiceTest
    {
        
        [TestMethod]
        public async Task Get_All_Products_Exist()
        {
            Mock<IProductRepository> _mockProductRepository = new Mock<IProductRepository>();
            _mockProductRepository.Setup(r => r.GetAll())
                .ReturnsAsync(() => new List<Product>()
                    {
                        new Product(){ Id = Guid.NewGuid(), Name="Bill"},
                        new Product(){ Id = Guid.NewGuid(), Name="Steve"},
                        new Product(){ Id = Guid.NewGuid(), Name="Ram"},
                        new Product(){ Id = Guid.NewGuid(), Name="Abdul"}
                    });

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.Products).Returns(_mockProductRepository.Object);

            var productService = new ProductService(mockUnitOfWork.Object);
            var results = await productService.GetAll();

            Assert.IsNotNull(results);
        }

        [TestMethod]
        public async Task Get_All_Products_Not_Exist()
        {
            Mock<IProductRepository> _mockProductRepository = new Mock<IProductRepository>();
            _mockProductRepository.Setup(r => r.GetAll())
                .ReturnsAsync(() => new List<Product>() { });

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.Products).Returns(_mockProductRepository.Object);

            var productService = new ProductService(mockUnitOfWork.Object);
            var results = await productService.GetAll();

            Assert.IsTrue(results.Count == 0);
        }

        [TestMethod]
        public async Task Get_Product_Exist()
        {
            var id = Guid.NewGuid();
            Mock<IProductRepository> _mockProductRepository = new Mock<IProductRepository>(); 
            _mockProductRepository.Setup(r => r.Find(It.IsAny<Guid>()))
                .Returns(() => new Product() { Id = id, Name = "Bill" });

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.Products).Returns(_mockProductRepository.Object);

            var productService = new ProductService(mockUnitOfWork.Object);
            var results = await productService.GetById(id);

            Assert.IsNotNull(results);
        }
    }
}
