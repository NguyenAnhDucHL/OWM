using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using OMW_Project.Models;

namespace OMW_Project.Repositories
{
    public class OrderRepository:IOrderRepository
    {
        public readonly ProjectDbContext db;

        public OrderRepository()
        {
            db = new ProjectDbContext();
        }
        public void Dispose()
        {
            db.Dispose();
        }

        public Order Find(string orderId)
        {
            return db.Orders.Include(o => o.User).Include(o =>o.OrderProducts.Select(x => x.Product)).Include(o=>o.Doctor).FirstOrDefault(c => c.OrderId.Equals(orderId));
        }

        public void Add(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
        }

        public void Update(Order order)
        {
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IList<Order> GetAll()
        {
            return db.Orders.Include(o=>o.User).ToList();
        }

        public IList<Order> GetAllProcessed()
        {
            return db.Orders.Include(o => o.User).Where(c=>c.Status).ToList();
        }

        public IList<Order> GetAllNoProcessed()
        {
            return db.Orders.Include(o => o.User).Where(c => c.Status!=true).ToList();
        }

        public IList<Order> GetHistoryByPatient(string patientId)
        {
            return db.Orders.Include(o => o.OrderProducts.Select(x=>x.Product)).Where(o => o.UserId.Equals(patientId)).ToList();
        }

        public Order GetOrderByDoctorPaymentId(string patientId)
        {
            return null;
        }

        public void Remove(string oderId)
        {
            var order = db.Orders.Find(oderId);
            db.Orders.Remove(order);
            db.SaveChanges();
        }

        public IList<Order> GetAllThisMonth()
        {
            var date = DateTime.Now;
            return db.Orders.Include(c=>c.OrderProducts).Include(c => c.User).Include(c => c.Doctor).Where(c=>c.OrderTime.Year==date.Year && c.OrderTime.Month==date.Month && c.Status==true).ToList();
        }
    }
}