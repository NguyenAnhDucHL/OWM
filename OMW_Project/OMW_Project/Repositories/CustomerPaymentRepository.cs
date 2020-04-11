using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMW_Project.Models;


namespace OMW_Project.Repositories
{
    public class CustomerPaymentRepository:ICustomerPaymentRepository
    {
        public readonly ProjectDbContext db;
        public CustomerPaymentRepository()
        {
            db = new ProjectDbContext();
        }
        public void Dispose()
        {
            db.Dispose();
        }


        public CustomerPayment Find(string customerPaymentId)
        {
            return db.CustomerPayments.FirstOrDefault(c => c.CustomerPaymentId.Equals(customerPaymentId));
        }

        public void Add(CustomerPayment customerPayment)
        {
            db.CustomerPayments.Add(customerPayment);
            db.SaveChanges();
        }

        public void Update(CustomerPayment customerPayment)
        {
            db.Entry(customerPayment).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IList<CustomerPayment> GetAll()
        {
            return db.CustomerPayments.ToList();
        }
    }
}
