using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMW_Project.Models;

namespace OMW_Project.Repositories
{
    public interface ICategoryPostRepository:IDisposable
    {
        CategoryPost Find(string categoryPostId);
        void Add(CategoryPost categoryPost);
        void Update(CategoryPost categoryPost);
        IList<CategoryPost> GetAll();
    }
}
