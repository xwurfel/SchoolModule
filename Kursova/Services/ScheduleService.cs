using Kursova.Entity_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova.Services
{
    internal class ScheduleService
    {
        EntityLogicContext db;

        public ScheduleService()
        {
            db = new EntityLogicContext();
        }

        public ScheduleService(DateTime dateTime, Const.Subject subject, int group, int course, string professor)//Professor professor)
        {
            db = new EntityLogicContext();
            LessonsDate lessonsDate = new LessonsDate() { Course = course, ProfessorName = professor, Subject = subject, DateTime = dateTime, Group = group };
            db.dates.Add(lessonsDate);
            db.SaveChanges();
        }

        public void AddDate(DateTime dateTime, Const.Subject subject, int group, int course, string professor)//Professor professor)
        {
            LessonsDate lessonsDate = new LessonsDate() { Course = course, ProfessorName = professor, Subject = subject, DateTime = dateTime, Group = group };
            db.dates.Add(lessonsDate);
            db.SaveChanges();
        }
        public void ClearOldDates()
        {
            db.dates.RemoveRange(db.dates.Where(x => x.DateTime < DateTime.Now));
            db.SaveChanges();
        }

        public void ClearSchedules()
        {
            db.dates.RemoveRange(db.dates);
            db.SaveChanges();
        }

    }
}
