using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagement.Data;

namespace ProductManagement.EntityFramework
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {        
        public ProductRepository(DatabaseEntities context) : base(context)
        {
        }
    }
}

