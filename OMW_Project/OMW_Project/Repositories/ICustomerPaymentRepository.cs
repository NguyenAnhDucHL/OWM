using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMW_Project.Models;


namespace OMW_Project.Repositories
{
    public interface ICustomerPaymentRepository:IDisposable
    {
        CustomerPayment Find(string customerPaymentId);
        void Add(CustomerPayment customerPayment);
        void Update(CustomerPayment customerPayment);
        IList<CustomerPayment> GetAll();
    }
}
