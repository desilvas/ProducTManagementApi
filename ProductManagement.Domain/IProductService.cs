using ProductManagement.Domain.DomainModels;
using ProductManagement.EntityFramework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Domain
{
    public interface IProductService
    {
        Task<List<ProductDomainModel>> GetAll();

        Task<List<ProductDomainModel>> GetByName(string name);

        Task<List<ProductDomainModel>> GetById(Guid id);

        Product Add(Product product);

        Product Update(Product product);
        bool Delete(Guid Id);
        bool AddProductOption(ProductOptionsModel productOption);

        Task<List<ProductOptionsModel>> GetProductOptionsById(Guid id);

        Task<List<ProductOptionsModel>> GetProductOptionsByIdAndProductId(Guid productId, Guid optionId);

        bool DeleteProductOption(Guid productId, Guid optionId);
    }
}
