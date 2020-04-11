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

        public IList<Order> GetHistoryByPatient(string patientId)
        {
            return db.Orders.Include(o => o.OrderProducts.Select(x=>x.Product)).Where(o => o.UserId.Equals(patientId)).ToList();
        }
    }
}