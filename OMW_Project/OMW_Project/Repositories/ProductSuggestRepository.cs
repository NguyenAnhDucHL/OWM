using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using OMW_Project.Models;

namespace OMW_Project.Repositories
{
    public class ProductSuggestRepository:IProductSuggestRepository
    {
        public readonly ProjectDbContext db;

        public ProductSuggestRepository()
        {
            db = new ProjectDbContext();
        }
        public void Dispose()
        {
            db.Dispose();
        }

        public void Add(ProductSuggest productSuggest)
        {
            db.ProductSuggests.Add(productSuggest);
            db.SaveChanges();
        }

        public void Update(ProductSuggest productSuggest)
        {
            db.Entry(productSuggest).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IList<ProductSuggest> GetAll()
        {
            return db.ProductSuggests.ToList();
        }
    }
}