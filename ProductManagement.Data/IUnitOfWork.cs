using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IProductOptionRepository ProductsOption { get; }
        int Commit();
    }
}
