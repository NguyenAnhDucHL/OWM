using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using OMW_Project.Models;

namespace OMW_Project.Repositories
{
    public class UserRepository:IUserRepository
    {
        public readonly ProjectDbContext db;

        public UserRepository()
        {
            db = new ProjectDbContext();
        }
        public void Dispose()
        {
            db.Dispose();
        }

        public User Find(string userId)
        {
            return db.Users.FirstOrDefault(c => c.Id.Equals(userId));
        }

        public void Add(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IList<User> GetAll()
        {
            return db.Users.Include(u=>u.Roles).ToList();
        }

        public IList<User> GetAllDoctor()
        {
            var role = db.Roles.FirstOrDefault(x => x.Name.Equals("Doctor"));
            var doctors=db.Users.Where(d=>d.Roles.Any(r=>r.RoleId.Equals(role.Id)));
            return doctors.ToList();
        }
    }
}