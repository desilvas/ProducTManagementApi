using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagement.EntityFramework;

namespace ProductManagement.Domain.DomainModels
{
    public class ProductOptionsModel
    {
        
        public Guid Id { get; set; }
       
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ProductOptionsModel()
        {

        }

        public ProductOptionsModel(ProductOption productOption)
        {
            Id = productOption.Id;
            Name = productOption.Name;
            Description = productOption.Description;
        }
    }
}
