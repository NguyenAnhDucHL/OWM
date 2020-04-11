using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMW_Project.Models;


namespace OMW_Project.Repositories
{
    public class ConsultingRepository : IConsultingRepository
    {
        public readonly ProjectDbContext db;
        public ConsultingRepository()
        {
            db = new ProjectDbContext();
        }
        public void Dispose()
        {
            db.Dispose();
        }

        public Consulting Find(string consultingId)
        {
            return db.Consultings.Include(m => m.Doctor).Include(m => m.Patient).AsNoTracking().FirstOrDefault(c => c.ConsultingId.Equals(consultingId));
        }

        public void Add(Consulting consulting)
        {
            db.Consultings.Add(consulting);
            db.SaveChanges();
        }

        public void Update(Consulting consulting)
        {
            var old = db.Consultings.Find(consulting.ConsultingId);
            db.Entry(old).CurrentValues.SetValues(consulting);
            db.SaveChanges();
        }

        public IList<Consulting> GetAll()
        {
            return db.Consultings.Include(m => m.Doctor).Include(c => c.Patient).ToList();
        }

        public void Remove(string consultingId)
        {
            var con = db.Consultings.Find(consultingId);
            db.Consultings.Remove(con);
            db.SaveChanges();
        }

        public IList<Consulting> GetAllMet()
        {
            var now = DateTime.Now.AddHours(1);
            return db.Consultings.Include(c => c.Doctor).Where(c => c.Status && c.StartConsulting < now).ToList();
        }

        public IList<Consulting> GetAllNotMet()
        {
            var now = DateTime.Now.AddHours(1);
            return db.Consultings.Include(c => c.Doctor).Where(c => c.Status && c.StartConsulting > now).ToList();
        }

        public IList<Consulting> GetAllForBook(string docId)
        {
            return db.Consultings.Include(c => c.Doctor).Where(c => DbFunctions.DiffDays(c.StartConsulting, DateTime.Now) < 0 && !c.Status && c.DoctorId.Equals(docId)).ToList();
        }

        public IList<User> GetAllDoctorByTime(DateTime dateTime)
        {
            return db.Consultings.Include(c => c.Doctor).Where(c => c.StartConsulting == dateTime && !c.Status).Select(c => c.Doctor).ToList();
        }

        public void SaveBook(Consulting consulting, string patientId)
        {
            var item = db.Consultings.Find(consulting.ConsultingId);
            item.Status = true;
            item.PatientIssue = consulting.PatientIssue;
            item.PatientId = patientId;
            db.SaveChanges();
        }

        public IList<Consulting> GetHistoryByPatient(string patientId)
        {
            return db.Consultings.Include(c=>c.Doctor).Include(m => m.Patient).Where(c => c.PatientId.Equals(patientId)).OrderBy(c=>c.StartConsulting).ToList();
        }

        public IList<Consulting> GetAllAvalable()
        {
            return db.Consultings.Include(c => c.Doctor).Where(c => DbFunctions.DiffDays(c.StartConsulting, DateTime.Now) < 0 && !c.Status).ToList();
        }

        public IList<Consulting> GetAvailableByDoctorId(string doctorId)
        {
            return db.Consultings.Where(c => DbFunctions.DiffDays(c.StartConsulting, DateTime.Now) < 0 && c.DoctorId.Equals(doctorId)).ToList();
        }

        public IList<Consulting> GetAllMetByDoctorIdResult(string docId)
        {
            return db.Consultings.Include(c=>c.Patient).Where(c => DbFunctions.DiffDays(c.StartConsulting, DateTime.Now) >= 0 && c.DoctorId.Equals(docId) && c.Status && !db.ConsultResults.Any(r=>r.ConsultingId.Equals(c.ConsultingId))).ToList();
        }

        public IList<Consulting> GetAllMetByDoctorIdHistory(string docId)
        {
            throw new NotImplementedException();
        }
    }
}
