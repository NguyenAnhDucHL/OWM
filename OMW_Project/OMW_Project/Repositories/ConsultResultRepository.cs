using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMW_Project.Models;


namespace OMW_Project.Repositories
{
    public class ConsultResultRepository:IConsultResultRepository
    {
        public readonly ProjectDbContext db;
        public ConsultResultRepository()
        {
            db = new ProjectDbContext();
        }
        public void Dispose()
        {
            db.Dispose();
        }

        public ConsultResult Find(string consultResultId)
        {
            return db.ConsultResults.FirstOrDefault(c => c.ConsultResultId.Equals(consultResultId));
        }

        public void Add(ConsultResult consultResult)
        {
            db.ConsultResults.Add(consultResult);
            db.SaveChanges();
        }

        public void Update(ConsultResult consultResult)
        {
            db.Entry(consultResult).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IList<ConsultResult> GetAll()
        {
            return db.ConsultResults.ToList();
        }

        public ConsultResult FindByConsultingId(string consultingtId)
        {
            return db.ConsultResults.Include(c=>c.Consulting.Doctor).Include(c=>c.ProductSuggests.Select(x=>x.Product)).Include(c=>c.ProductSuggests).FirstOrDefault(c => c.ConsultingId.Equals(consultingtId));
        }
    }
}
