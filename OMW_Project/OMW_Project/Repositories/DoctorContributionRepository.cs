using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using OMW_Project.Models;

namespace OMW_Project.Repositories
{
    public class DoctorContributionRepository:IDoctorContributionRepository
    {
        public readonly ProjectDbContext db;

        public DoctorContributionRepository()
        {
            db = new ProjectDbContext();
        }
        public void Dispose()
        {
            db.Dispose();
        }

        public void Add(DoctorContribution doctorContribution)
        {
            db.DoctorContributions.Add(doctorContribution);
            db.SaveChanges();
        }

        public void Update(DoctorContribution doctorContribution)
        {
            db.Entry(doctorContribution).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IList<DoctorContribution> GetAll()
        {
            return db.DoctorContributions.ToList();
        }

        public DoctorContribution FindByDoctorPaymentId(string doctorPaymentId)
        {
            return db.DoctorContributions.Include(c => c.DoctorPayment).Include(c => c.Order)
                .FirstOrDefault(c => c.DoctorPaymentId == doctorPaymentId);
        }
    }
}