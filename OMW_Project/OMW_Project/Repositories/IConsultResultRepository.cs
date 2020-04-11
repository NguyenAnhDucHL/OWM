using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMW_Project.Models;


namespace OMW_Project.Repositories
{
    public interface IConsultResultRepository:IDisposable
    {
        ConsultResult Find(string consultResultId);
        void Add(ConsultResult consultResult);
        void Update(ConsultResult consultResult);
        IList<ConsultResult> GetAll();
    }
}
