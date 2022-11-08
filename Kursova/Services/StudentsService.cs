using Kursova.Const;
using Kursova.Entity_Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova.Services
{
    /// <summary>
    /// Class that operates with Entity Student.
    /// 
    /// Has EntityLogicContext field to operate with database.
    /// Has 6 methods to operate with Student Entity.
    /// </summary>
    internal class StudentsService
    {
        EntityLogicContext db;

        public StudentsService()
        {
            db = new EntityLogicContext();
        }

        public StudentsService(string Name, int GroupId)
        {
            db = new EntityLogicContext();
            Student s = new Student() { Group = db.groups.First(x => x.Id.Equals(GroupId)), Name = Name};
            db.students.Add(s);
            db.SaveChanges();
        }

        public void AddStudent(string Name, int GroupId)
        {
            Student s = new Student() { Group = db.groups.First(x => x.Id.Equals(GroupId)), Name = Name };
            db.students.Add(s);
            db.SaveChanges();
        }

        
        public List<Student> GetStudentsByProfessor(Professor professor)
        {
            if (professor == null)
                throw new Exception("Professor must not be null!");
           return db.dates.Include(x => x.Professor).Where(x => x.Professor == professor).Include(x => x.Group).Include(x => x.Group.students).SelectMany(x => x.Group.students).ToList();
        }



        public List<Student> GetStudentsByCourse(Course course)
        {
            if (course == null)
                throw new Exception("Course must not be null!");
            return db.students.Include(g => g.Group).Include(g => g.Group.Course).Where(x => x.Group.Course.Equals(course)).ToList();
        }




        public void RemoveStudent(string name)
        {
            if (name == null)
                throw new ArgumentNullException(name);
            db.students.Remove(db.students.First(x => x.Name.Equals(name)));
            db.SaveChanges();
        }


        public void ClearStudents()
        {
            db.students.RemoveRange(db.students);
            db.SaveChanges();
        }
    }
}
