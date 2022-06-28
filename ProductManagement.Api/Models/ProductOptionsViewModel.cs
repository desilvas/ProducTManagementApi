using Newtonsoft.Json;
using ProductManagement.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ProductManagement.Api.Models
{
    public class ProductOptionsViewModel
    {
        
        [JsonIgnore]
        [IgnoreDataMember]
        public Guid Id { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ProductOptionsViewModel()
        {

        }

        public ProductOptionsViewModel(ProductOptionsModel productOptions)
        {
            Id = productOptions.Id;
            ProductId = productOptions.ProductId;
            Name = productOptions.Name;
            Description = productOptions.Description;
        }

        public ProductOptionsModel ToDataMode(Guid productId)
        {
            return new ProductOptionsModel
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                Name = Name,
                Description = Description
            };
            
        }
    }
}