using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using OMW_Project.Models;

namespace OMW_Project.Repositories
{
    public class ProductRepository:IProductRepository
    {
        public readonly ProjectDbContext db;

        public ProductRepository()
        {
            db = new ProjectDbContext();
        }
        public void Dispose()
        {
            db.Dispose();
        }

        public Product Find(string productId)
        {
            return db.Products.Include(m=>m.CategoryProduct).FirstOrDefault(c => c.ProductId.Equals(productId));
        }

        public void Add(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
        }

        public void Update(Product product)
        {
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IList<Product> GetAll()
        {
            return db.Products.Include(p => p.CategoryProduct).Include(p => p.User).ToList();
        }
        public void Remove(string productId)
        {
            var pr = db.Products.Find(productId);
            db.Products.Remove(pr);
            db.SaveChanges();
        }

        public IList<Product> GetByCategory(string categoryId)
        {
            return db.Products.Where(p => categoryId.Equals(p.CategoryId)).ToList();
        }
    }
}