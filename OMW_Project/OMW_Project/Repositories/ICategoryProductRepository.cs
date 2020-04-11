using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMW_Project.Models;


namespace OMW_Project.Repositories
{
    public interface ICategoryProductRepository : IDisposable
    {
        CategoryProduct Find(string categoryId);
        void Add(CategoryProduct category);
        void Update(CategoryProduct category);
        IList<CategoryProduct> GetAll();
       
    }
}
