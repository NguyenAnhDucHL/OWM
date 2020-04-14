using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OMW_Project.Models;

namespace OMW_Project.Repositories
{
    public interface IDoctorContributionRepository:IDisposable
    {
        void Add(DoctorContribution doctorContribution);
        void Update(DoctorContribution doctorContribution);
        IList<DoctorContribution> GetAll();
        DoctorContribution FindByDoctorPaymentId(string doctorPaymentId);
    }
}