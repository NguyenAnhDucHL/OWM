using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using OMW_Project.Models;

namespace OMW_Project.Repositories
{
    public class DoctorPaymentRepository:IDoctorPaymentRepository
    {
        public readonly ProjectDbContext db;

        public DoctorPaymentRepository()
        {
            db = new ProjectDbContext();
        }
        public void Dispose()
        {
            db.Dispose();
        }


        public DoctorPayment Find(string doctorPaymentId)
        {
            return db.DoctorPayments.FirstOrDefault(c => c.DoctorPaymentId.Equals(doctorPaymentId));
        }

        public void Add(DoctorPayment doctorPayment)
        {
            db.DoctorPayments.Add(doctorPayment);
            db.SaveChanges();
        }

        public void Update(DoctorPayment doctorPayment)
        {
            db.Entry(doctorPayment).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IList<DoctorPayment> GetAll()
        {
            return db.DoctorPayments.ToList();
        }
    }
}