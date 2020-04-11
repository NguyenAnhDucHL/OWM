using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using OMW_Project.Models;

namespace OMW_Project.Repositories
{
    public class CategoryPostRepository:ICategoryPostRepository
    {
        public readonly ProjectDbContext db;

        public CategoryPostRepository()
        {
            db = new ProjectDbContext();
        }
        public void Dispose()
        {
            db.Dispose();
        }

        public CategoryPost Find(string categoryPostId)
        {
            return db.CategoryPosts.FirstOrDefault(c => c.CategoryPostId.Equals(categoryPostId));
        }

        public void Add(CategoryPost categoryPost)
        {
            db.CategoryPosts.Add(categoryPost);
            db.SaveChanges();
        }

        public void Update(CategoryPost categoryPost)
        {
            db.Entry(categoryPost).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IList<CategoryPost> GetAll()
        {
           return db.CategoryPosts.ToList();
        }
    }
}