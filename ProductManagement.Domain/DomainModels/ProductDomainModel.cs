using ProductManagement.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Domain.DomainModels
{
    public class ProductDomainModel
    {      

        public ProductDomainModel(Product domain)
        {
            Id = domain.Id;
            Name = domain.Name;
            Description = domain.Description;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DeliveryPrice { get; set; }

        public Product ToDataModel()
        {
            return new Product()
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Price = Price,
                DeliveryPrice = DeliveryPrice
            };

        }
    }
}
