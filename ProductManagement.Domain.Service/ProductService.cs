using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagement.Data;
using ProductManagement.Domain.DomainModels;
using ProductManagement.EntityFramework;

namespace ProductManagement.Domain.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ProductDomainModel>> GetAll()
        {
            var result = await _unitOfWork.Products.GetAll();
            return result.Select(product => new ProductDomainModel(product))
                            .ToList();
        }

        public async Task<List<ProductDomainModel>> GetByName(string name)
        {
            var result = await _unitOfWork.Products.Find(x => x.Name.Contains(name));
            return result.Select(product => new ProductDomainModel(product))
                            .ToList();
        }

        public async Task<List<ProductDomainModel>> GetById(Guid id)
        {
            var result = await _unitOfWork.Products.Find(x => x.Id == id);
            return result.Select(product => new ProductDomainModel(product))
                            .ToList();
        }

        public Product Add(Product product)
        {
            var results = _unitOfWork.Products.Add(product);
            _unitOfWork.Commit();

            return results;
        }

        public Product Update(Product update)
        {
            var product = _unitOfWork.Products.Find(update.Id);

            if (update != null)
            {
                _unitOfWork.Products.Update(product, update);

                _unitOfWork.Commit();
                return update;
            }
            return null;
        }

        public bool Delete(Guid Id)
        {
            var update = _unitOfWork.Products.Find(Id);
            if (update != null)
            {
                var task = Task.Run(async () => await _unitOfWork.ProductsOption.Find(i => i.ProductId == Id));
                var productOptions = task.Result.ToList();

                foreach (var item in productOptions)
                {
                    _unitOfWork.ProductsOption.Remove(item);
                }
                _unitOfWork.Products.Remove(update);
                _unitOfWork.Commit();
                return true;
            }
            return false;
        }

        public bool AddProductOption(ProductOptionsModel productOption)
        {
            var product = _unitOfWork.Products.Find(productOption.ProductId);

            if (product != null)
            {
                var model = new ProductOption
                {
                    Id = productOption.Id,
                    ProductId = productOption.ProductId,
                    Name = productOption.Name,
                    Description = productOption.Description
                };
                _unitOfWork.ProductsOption.Add(model);

                _unitOfWork.Commit();
                return true;
            }
            return false;
        }

        public async Task<List<ProductOptionsModel>> GetProductOptionsById(Guid id)
        {
            var result = await _unitOfWork.ProductsOption.Find(x => x.ProductId == id);
            return result.Select(productOption => new ProductOptionsModel(productOption))
                            .ToList();
        }

        public async Task<List<ProductOptionsModel>> GetProductOptionsByIdAndProductId(Guid productId, Guid optionId)
        {
            var result = await _unitOfWork.ProductsOption.Find(x => x.ProductId == productId && x.Id == optionId);
            return result.Select(productOption => new ProductOptionsModel(productOption))
                            .ToList();
        }

        public bool DeleteProductOption(Guid productId, Guid optionId)
        {
            var task = Task.Run(async () => await
            _unitOfWork.ProductsOption.Find(i => i.ProductId == productId && i.Id == optionId));
            var productOptions = task.Result.Single();
            if (productOptions != null)
            {
                _unitOfWork.ProductsOption.Remove(productOptions);
                _unitOfWork.Commit();
                return true;
            }
            return false;
        }
    }

}
