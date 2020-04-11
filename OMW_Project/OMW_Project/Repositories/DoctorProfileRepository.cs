using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using OMW_Project.Models;

namespace OMW_Project.Repositories
{
    public class DoctorProfileRepository : IDoctorProfileRepository
    {

        public readonly ProjectDbContext db;

        public DoctorProfileRepository()
        {
            db = new ProjectDbContext();
        }
        public void Dispose()
        {
            db.Dispose();
        }

        public DoctorProfile Find(string userId)
        {
            return db.DoctorProfiles.Include(x=>x.User).FirstOrDefault(c => c.UserId.Equals(userId));
        }

        public void Add(DoctorProfile DoctorProfile)
        {
            db.DoctorProfiles.Add(DoctorProfile);
            db.SaveChanges();
        }

        public void Update(DoctorProfile DoctorProfile)
        {
            db.Entry(DoctorProfile).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IList<DoctorProfile> GetAll()
        {
            return db.DoctorProfiles.Include(d=>d.User).ToList();
        }

        public void Remove(string id)
        {
            var doc = db.DoctorProfiles.FirstOrDefault(d => d.UserId.Equals(id));
            if (doc != null)
            {
                db.DoctorProfiles.Remove(doc);
                db.SaveChanges();
            }

        }
    }
}