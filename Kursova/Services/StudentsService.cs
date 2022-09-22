using Kursova.Const;
using Kursova.Entity_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova.Services
{
    internal class StudentsService
    {
        EntityLogicContext db;

        public StudentsService()
        {
            db = new EntityLogicContext();
        }

        public StudentsService(string name, int courseNumber, int groupNumber, List<Subject> subjects)
        {
            db = new EntityLogicContext();
            Student s = new Student() { CourseNumber = courseNumber, GroupNumber = groupNumber, Name = name, Subjects = subjects };
            db.students.Add(s);
            db.SaveChanges();
        }

        public void AddStudent(string name, int courseNumber, int groupNumber, List<Subject> subjects)
        {
            Student s = new Student() { CourseNumber = courseNumber, GroupNumber = groupNumber, Name = name, Subjects = subjects };
            db.students.Add(s);
            db.SaveChanges();

        }
        public List<Student> GetStudentsByProfessor(Professor professor)
        {
            return db.students.ToList().Where(x => professor.Courses.Contains(x.CourseNumber)).ToList();
        }

        public List<Student> GetStudentsByCourse(int course)
        {
            return db.students.ToList().Where(x => x.CourseNumber == course).ToList();
        }

        public void ClearOldDates()
        {
            
        }


        public void RemoveStudent(string name)
        {
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
