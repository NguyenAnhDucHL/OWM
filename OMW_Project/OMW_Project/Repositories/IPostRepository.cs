using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMW_Project.Models;

namespace OMW_Project.Repositories
{
    public interface IPostRepository:IDisposable
    {
        Post Find(string postId);
        void Add(Post post);
        void Update(Post post);
        void Remove(string postId);
        IList<CategoryPost> GetPost_Category();
        List<Post> GetDetail_Category(string id);
        Post GetPostDetail(string id);
        IList<Post> GetAll();
        IList<Post> GetApproved();
        IList<Post> GetUnApproved();
    }
}
