using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMW_Project.Models;


namespace OMW_Project.Repositories
{
    public interface IConsultingRepository : IDisposable
    {
        Consulting Find(string consultingId);
        void Add(Consulting consulting);
        void Update(Consulting consulting);
        void Remove(string consultingId);
        IList<Consulting> GetAll();
        IList<Consulting> GetAllAvalable();
        IList<Consulting> GetAllMet();
        Consulting CheckUserId(string userId);
        IList<Consulting> GetAllMetByDoctorIdResult(string docId);
        IList<Consulting> GetAllMetByDoctorIdHistory(string docId);
        IList<Consulting> GetHistoryByPatientPresent(string patientId);
        IList<Consulting> GetAllNotMet();
        IList<Consulting> GetAllForBook(string docId);
        IList<User> GetAllDoctorByTime(DateTime dateTime);
        IList<Consulting> GetHistoryByPatient(string patientId);
        IList<Consulting> GetAvailableByDoctorId(string doctorId);
        void SaveBook(Consulting consulting, string patientId);

    }
}
