using ProductManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseEntities _context;
        public UnitOfWork(DatabaseEntities context)
        {
            _context = context;
            Products = new ProductRepository(_context);
            ProductsOption = new ProductOptionRepository(_context);
        }
        public IProductRepository Products { get; private set; }
        public IProductOptionRepository ProductsOption { get; private set; }        

        public int Commit()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
