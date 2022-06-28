using ProductManagement.Domain.DomainModels;
using ProductManagement.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductManagement.Api.Models
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {

        }
        public ProductViewModel(ProductDomainModel model)
        {
            this.Id = model.Id;
            this.Name = model.Name;
            this.Price = model.Price;
            this.DeliveryPrice = model.DeliveryPrice;
            this.Description = model.Description;
        }

        
        public Guid? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal DeliveryPrice { get; set; }

        public Product ToDataModel()
        {
            return new Product()
            {
                Id = Guid.NewGuid(),
                Name = Name,
                Description = Description,
                Price = Price,
                DeliveryPrice = DeliveryPrice
            };

        }
    }
}