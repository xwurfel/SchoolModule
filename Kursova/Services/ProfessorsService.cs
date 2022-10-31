using Kursova.Entity_Framework;
using System.Collections.Generic;
using System.Linq;
using Kursova.Const;
using System;
using System.Xml.Linq;
using System.Data.Entity;

namespace Kursova.Services
{
    internal class ProfessorsService
    {
        EntityLogicContext db;

        public ProfessorsService()
        {
            db = new EntityLogicContext();
        }
        public ProfessorsService(string name, List<Const.Subject>? subjects, Const.Position Position, int Experience)
        {
            db = new EntityLogicContext();
            Professor p = new () { Name = name, Experience = Experience, Subjects = subjects, Position = Position};
            db.professors.Add(p);
            db.SaveChanges();
        }

        public void AddProfessor(string name, List<Const.Subject>? subjects, Const.Position Position, int Experience)
        {
            Professor p = new() { Name = name, Experience = Experience, Subjects = subjects, Position = Position };

            db.professors.Add(p);
            db.SaveChanges();
        }

        public void AddProfessor(Professor p)
        {
            Professor pr = new() { Name = p.Name, Experience = p.Experience, Subjects = p.Subjects, Position = p.Position };

            db.professors.Add(pr);
            db.SaveChanges();
        }

        public void RemoveProfessor(string name)
        {
            db.professors.Remove(db.professors.First(x => x.Name.Equals(name)));
            db.SaveChanges();
        }

        public List<Professor> GetAll()
        {
            return db.professors.ToList();
        }

        public Professor? GetByName(string Name)
        {
            if(Name == null)
                throw new ArgumentNullException("name");
            return db.professors.FirstOrDefault(x => x.Name.Equals(Name));
        }

        public List<Professor> GetBySubject(Subject subject)
        {
            return db.professors.ToList().Where(x => x.Subjects.Contains(subject)).ToList();
        }

        
        public Professor? GetMostPopularBySubject(Subject subject)
        {
            return db.professors.First(x => x.Id.Equals(db.dates.Include(p => p.Professor).GroupBy(x => x.Professor.Id).OrderBy(x => x.Count()).Select(x => x.Key).First()));
        }

        public List<Professor> SortByPositionThenByName()
        {
            return db.professors.ToList().OrderBy(x => x.Position).ThenBy(x => x.Name).ToList();
        }


        //course == subject? task error
        public List<Professor> GetWithOnlyOneCourse()
        {
            return db.professors.ToList().Where(x => x.Subjects.Count == 1).ToList();
        }


        public List<Professor> GetFreeByDate(DateTime dateTime)
        {
            return db.dates.Include(p => p.Professor).Where(x => x.DateTime != dateTime).Select(x => x.Professor).ToList();
        }


        public void ClearProfessors()
        {
            db.professors.RemoveRange(db.professors);
            db.SaveChanges();
        }
        
    }
}
