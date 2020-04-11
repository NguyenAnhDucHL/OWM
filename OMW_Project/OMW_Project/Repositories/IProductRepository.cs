using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMW_Project.Models;

namespace OMW_Project.Repositories
{
    public interface IProductRepository:IDisposable
    {
        Product Find(string productId);
        void Add(Product product);
        void Update(Product product);
        void Remove(string productId);
        IList<Product> GetAll();
        IList<Product> GetByCategory(string categoryId);
    }
}
