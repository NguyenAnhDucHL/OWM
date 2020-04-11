using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMW_Project.Models;


namespace OMW_Project.Repositories
{
    public class CategoryProductRepository : ICategoryProductRepository
    {
        public readonly ProjectDbContext db;

        public CategoryProductRepository()
        {
            db = new ProjectDbContext();
        }
        public void Dispose()
        {
            db.Dispose();
        }

        public CategoryProduct Find(string categoryId)
        {
            return db.CategoryProducts.FirstOrDefault(c => c.CategoryProductId.Equals(categoryId));
        }

        public void Add(CategoryProduct category)
        {
            db.CategoryProducts.Add(category);
            db.SaveChanges();
        }

        public void Update(CategoryProduct category)
        {
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
        }


        public IList<CategoryProduct> GetAll()
        {
            return db.CategoryProducts.ToList();
        }

        
    }
}
