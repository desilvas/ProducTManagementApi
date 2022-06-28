using ProductManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.EntityFramework
{
    public class ProductOptionRepository : BaseRepository<ProductOption>, IProductOptionRepository
    {
        public ProductOptionRepository(DatabaseEntities context) : base(context)
        {

        }
    }
}
