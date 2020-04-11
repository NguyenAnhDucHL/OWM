using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMW_Project.Models;

namespace OMW_Project.Repositories
{
    public interface IUserRepository:IDisposable
    {
        User Find(string userId);
        void Add(User user);
        void Update(User user);
        IList<User> GetAll();
        IList<User> GetAllDoctor();
    }
}
