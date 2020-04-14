using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMW_Project.Models;

namespace OMW_Project.Repositories
{
    public interface IDoctorPaymentRepository : IDisposable
    {
        DoctorPayment Find(string doctorPaymentId);
        void Add(DoctorPayment doctorPayment);
        void Update(DoctorPayment doctorPayment);
        IList<DoctorPayment> GetAll();
        IList<DoctorPayment> GetAllDoctorId(string doctorId);
    }
}
