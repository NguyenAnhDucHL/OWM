using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMW_Project.Models;

namespace OMW_Project.Repositories
{
    public interface IOrderRepository:IDisposable
    {
        Order Find(string orderId);
        void Add(Order order);
        void Update(Order order);
        IList<Order> GetAll();
        IList<Order> GetAllProcessed(); 
        IList<Order> GetAllNoProcessed();
        IList<Order> GetHistoryByPatient(string patientId);
        Order GetOrderByDoctorPaymentId(string patientId);
        void Remove(string oderId);
        IList<Order> GetAllThisMonth();

    }
}
