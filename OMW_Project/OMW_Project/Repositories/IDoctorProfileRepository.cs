using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMW_Project.Models;

namespace OMW_Project.Repositories
{
    public interface IDoctorProfileRepository:IDisposable
    {
        DoctorProfile Find(string userId);
        void Add(DoctorProfile DoctorProfile);
        void Update(DoctorProfile DoctorProfile);
        IList<DoctorProfile> GetAll();

        void Remove(string id);
    }
}
