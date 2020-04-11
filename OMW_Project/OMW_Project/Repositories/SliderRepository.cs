using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using OMW_Project.Models;

namespace OMW_Project.Repositories
{
    public class SliderRepository:ISliderRepository
    {
        public readonly ProjectDbContext db;

        public SliderRepository()
        {
            db = new ProjectDbContext();
        }
        public void Dispose()
        {
            db.Dispose();
        }

        public void Add(Slider slider)
        {
            db.Sliders.Add(slider);
            db.SaveChanges();
        }

        public void Update(Slider slider)
        {
            db.Entry(slider).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IList<Slider> GetAll()
        {
            return db.Sliders.ToList();
        }
    }
}