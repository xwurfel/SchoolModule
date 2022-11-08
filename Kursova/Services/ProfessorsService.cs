using Kursova.Entity_Framework;
using System.Collections.Generic;
using System.Linq;
using Kursova.Const;
using System;
using System.Xml.Linq;
using System.Data.Entity;
using System.Windows.Documents;

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


            Professor p = new () { 
                Name = name, 
                Experience = Experience, 
                Subjects = subjects, 
                Position = Position};


            db.professors.Add(p);
            db.SaveChanges();
        }

        public void AddProfessor(string name, List<Const.Subject>? subjects, Const.Position Position, int Experience)
        {
            Professor p = new() { 
                Name = name, 
                Experience = Experience, 
                Subjects = subjects, 
                Position = Position };


            db.professors.Add(p);
            db.SaveChanges();
        }

        public void AddProfessor(Professor p)
        {
            Professor pr = new() { 
                Name = p.Name, 
                Experience = p.Experience, 
                Subjects = p.Subjects, 
                Position = p.Position };


            db.professors.Add(pr);
            db.SaveChanges();
        }

        public void RemoveProfessor(string name)
        {
            if (name == null)
                throw new ArgumentNullException(name);


            db.professors
                .Remove(
                    db.professors.First(x => 
                        x.Name.Equals(name)));
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


            return db.professors
                .FirstOrDefault(x => 
                    x.Name.Equals(Name));
        }

        public List<Professor> GetBySubject(Subject subject)
        {
            return db.professors.ToList()
                .Where(x => x.Subjects
                    .Contains(subject))
                .ToList();
        }

        public Professor? GetMostPopularBySubject(Subject subject)
        {
            var profList = db.dates
                .Include(p => p.Professor)
                .Where(x => x.Subject
                    .Equals(subject))
                .Select(x => x.Professor.Id )
                .AsEnumerable();


            if (profList == null || profList.Count() == 0)
                throw new Exception();


            var query = from i in profList
                        group i by i into g
                        select new { g.Key, Count = g.Count() };

            var max = query.MaxBy(x => x.Count).Key;
            return db.professors
                .Where(x => x.Id.Equals(max))
                .First();
            
        }

        public List<Professor> SortByPositionThenByName()
        {
            return db.professors
                .ToList()
                .OrderBy(x => x.Position)
                .ThenBy(x => x.Name)
                .ToList();
        }


        public List<Professor> GetWithOnlyOneCourse()
        {
            return db.professors
                .ToList()
                .Where(x => x.Subjects.Count == 1)
                .ToList();
        }


        public List<Professor> GetFreeByDate(DateTime dateTime)
        {
            var list = db.dates
                .Include(p => p.Professor)
                .Select(x => new { ProfId = x.Professor.Id, date = x.DateTime })
                .ToList();

            var profIdList = list
                .Where(x => 
                    x.date.Equals(dateTime))
                .Select(x => x.ProfId);

            list.RemoveAll(x => 
                profIdList.Contains(x.ProfId));

            var l2 = list.Select(x => x.ProfId);

            return db.professors
                .Where(x => 
                    l2.Contains(x.Id))
                .ToList();
        }



        public void ClearProfessors()
        {
            db.professors.RemoveRange(db.professors);
            db.SaveChanges();
        }
        
    }
}
