using Kursova.Entity_Framework;
using System.Collections.Generic;
using System.Linq;
using Kursova.Const;
using System;

namespace Kursova.Services
{
    internal class ProfessorsService
    {
        EntityLogicContext db;

        public ProfessorsService()
        {
            db = new EntityLogicContext();
        }
        public ProfessorsService(string name, List<Subject> subjects, Position position, /*List<LessonsDate> schedule,*/ int experience, List<int> groups, List<int> courses)
        {
            db = new EntityLogicContext();
            Professor p = new () { Name = name, Subjects = subjects, Experience = experience, Position = position, /*Schedule = schedule,*/ Groups =  groups, Courses = courses  };
            //db.dates.AddRange(p.Schedule);
            db.professors.Add(p);
            db.SaveChanges();
        }

        public void AddProfessor(string name, List<Subject> subjects, Position position, /*List<LessonsDate> schedule,*/ int experience, List<int> groups, List<int> courses)
        {
            Professor p = new() { Name = name, Subjects = subjects, Experience = experience, Position = position, /*Schedule = schedule,*/ Groups = groups, Courses = courses };
            //db.dates.AddRange(p.Schedule);
            db.professors.Add(p);
            db.SaveChanges();
        }

        public void AddProfessor(Professor professor)
        {
            Professor p = new() { Name = professor.Name, Subjects = professor.Subjects, Experience = professor.Experience, Position = professor.Position, /*Schedule = professor.Schedule,*/ Groups = professor.Groups, Courses = professor.Courses };
            //db.dates.AddRange(p.Schedule);
            db.professors.Add(p);
            db.SaveChanges();
        }

        public void RemoveProfessor(string name)
        {
            db.professors.Remove(db.professors.First(x => x.Name.Equals(name)));
            db.SaveChanges();
        }

        public List<Professor> GetBySubject(Subject subject)
        {
            return db.professors.ToList().Where(x => x.Subjects.Contains(subject)).ToList();
        }

        public Professor? GetMostPopularBySubject(Subject subject)
        {
            return GetBySubject(subject).MaxBy(x => x.Groups.Count);
        }

        public List<Professor> SortByPositionThenByName()
        {
            return db.professors.ToList().OrderBy(x => x.Position).ThenBy(x => x.Name).ToList();
        }

        public List<Professor> GetWithOnlyOneCourse()
        {
            return db.professors.ToList().Where(x => x.Courses.Count == 1).ToList();
        }
        //TODO
        /*public List<Professor> GetFreeByDate(DateTime dateTime)
        {
            return db.professors.ToList().Where(x => !x.Schedule.Contains(new LessonsDate { DateTime = dateTime })).ToList();
        }*/


        

     
        public void ClearProfessors()
        {
            db.professors.RemoveRange(db.professors);
            db.SaveChanges();
        }
        
    }
}
