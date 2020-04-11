using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using OMW_Project.Models;

namespace OMW_Project.Repositories
{
    public class PostRepository:IPostRepository
    {
        public readonly ProjectDbContext db;

        public PostRepository()
        {
            db = new ProjectDbContext();
        }
        public void Dispose()
        {
            db.Dispose();
        }

        public Post Find(string postId)
        {
            return db.Posts.Include(p => p.User).Include(p=>p.CategoryPost).FirstOrDefault(c => c.PostId.Equals(postId));
        }

        public void Add(Post post)
        {
            db.Posts.Add(post);
            db.SaveChanges();
        }

        public void Update(Post post)
        {
            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IList<Post> GetAll()
        {
            return db.Posts.Include(p=>p.CategoryPost).Include(p=>p.User).ToList();
        }


        public IList<Post> GetApproved()
        {
            return db.Posts.Include(p => p.CategoryPost).Include(p => p.User).Where(p=>p.Status).ToList();
        }

        public IList<Post> GetUnApproved()
        {
            return db.Posts.Include(p => p.CategoryPost).Include(p => p.User).Where(p => !p.Status).ToList();
        }

        public void Remove(string postId)
        {
            var post = db.Posts.Find(postId);
            db.Posts.Remove(post);
            db.SaveChanges();
        }

        public IList<CategoryPost> GetPost_Category()
        {
            return db.CategoryPosts.ToList();
        }

        public List<Post> GetDetail_Category(string id)
        {
            return db.Posts.Where(p => p.CategoryPostId == id).ToList();
        }

        public Post GetPostDetail(string id)
        {
            return db.Posts.Where(p => p.PostId == id).FirstOrDefault();
        }
    }
}