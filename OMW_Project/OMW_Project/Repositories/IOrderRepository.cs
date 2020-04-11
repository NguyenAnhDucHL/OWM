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
        IList<Order> GetHistoryByPatient(string patientId);

        
    }
}
