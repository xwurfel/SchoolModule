using Kursova.Entity_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova.Services
{
    internal class DatesService
    {
        EntityLogicContext db;

        public DatesService()
        {
            db = new EntityLogicContext();
        }

        public DatesService(DateTime DateTime, Const.Subject Subject, int GroupId, int ProfessorId)
        {
            db = new EntityLogicContext();
            Dates dates = new Dates() { DateTime = DateTime, Group = db.groups.ToList().First(x => x.Id.Equals(GroupId)), Professor = db.professors.ToList().First(x => x.Id.Equals(ProfessorId)), Subject = Subject };
            db.dates.Add(dates);
            db.SaveChanges();
        }

        public void AddDate(DateTime DateTime, Const.Subject Subject, int GroupId, int ProfessorId)
        {
            Dates dates = new Dates() { DateTime = DateTime, Group = db.groups.ToList().First(x => x.Id.Equals(GroupId)), Professor = db.professors.ToList().First(x => x.Id.Equals(ProfessorId)), Subject = Subject };
            db.dates.Add(dates);
            db.SaveChanges();
        }

        public void RemoveDate(string groupName, DateTime date)
        {
            if (groupName == null)
                throw new ArgumentNullException(groupName);

            db.dates.Remove(db.dates.First(x => x.Group.Name.Equals(groupName) && x.DateTime.Equals(date)));
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
