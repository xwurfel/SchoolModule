using Kursova.Entity_Framework;
using System.Collections.Generic;
using System.Linq;
using Kursova.Const;

namespace Kursova.Services
{
    internal class ProfessorsService
    {
        EntityLogicContext db;

        public ProfessorsService()
        {
            db = new EntityLogicContext();
        }
        public ProfessorsService(string name, List<Subject> subjects, Position position, LessonsDate schedule, int experience, List<int> groups)
        {
            db = new EntityLogicContext();
            Professor p = new () { Name = name, Subjects = subjects, Experience = experience, Position = position, Schedule = schedule, Groups =  groups  };
            db.dates.Add(p.Schedule);
            db.professors.Add(p);
            db.SaveChanges();
        }

        public void AddProfessor(string name, List<Subject> subjects, Position position, LessonsDate schedule, int experience, List<int> groups)
        {
            Professor p = new() { Name = name, Subjects = subjects, Experience = experience, Position = position, Schedule = schedule, Groups = groups };
            db.dates.Add(p.Schedule);
            db.professors.Add(p);
            db.SaveChanges();
        }

        public void RemoveProfessor(string name)
        {
            db.professors.Remove(db.professors.First(x => x.Name.Equals(name)));
            db.SaveChanges();
        }

        public List<Professor> GetProfessorsBySubject(Subject subject)
        {
            return db.professors.ToList().Where(x => x.Subjects.Contains(subject)).ToList();
        }


        public void ClearProfessors()
        {
            db.professors.RemoveRange(db.professors);
            db.SaveChanges();
        }
        public void ClearSchedules()
        {
            db.dates.RemoveRange(db.dates);
            db.SaveChanges();
        }
    }
}
