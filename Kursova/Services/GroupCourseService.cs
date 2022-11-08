using Kursova.Entity_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kursova.Services
{
    internal class GroupCourseService
    {
        EntityLogicContext db;

        public GroupCourseService()
        {
            db = new EntityLogicContext();
        }

        public void AddCourse(string Name, List<Const.Subject>? Subjects)
        {
            Course s = new Course() { Name = Name, Subjects = Subjects };
            db.courses.Add(s);
            db.SaveChanges();
        }

        public void AddGroup(string Name, int CourseId)
        {
            Group s = new() { Name = Name, Course = db.courses.First(x => x.Id == CourseId) };
            db.groups.Add(s);
            db.SaveChanges();
        }

        public void RemoveGroup(string name)
        {
            if (name == null)
                throw new ArgumentNullException(name);
            db.groups.Remove(db.groups.First(x => x.Name.Equals(name)));
            db.SaveChanges();
        }

        public void RemoveCourse(string name)
        {
            if (name == null)
                throw new ArgumentNullException(name);
            db.courses.Remove(db.courses.First(x => x.Name.Equals(name)));
            db.SaveChanges();
        }

        public Group? GetGroupByName(string Name)
        {
            if (Name == null)
                throw new ArgumentNullException("name");
            return db.groups.FirstOrDefault(x => x.Name.Equals(Name));
        }

        public Course? GetCourseByName(string Name)
        {
            if (Name == null)
                throw new ArgumentNullException("name");
            return db.courses.FirstOrDefault(x => x.Name.Equals(Name));
        }

        public void Clear()
        {
            db.courses.RemoveRange(db.courses);
            db.professors.RemoveRange(db.professors);
            db.groups.RemoveRange(db.groups);
            db.dates.RemoveRange(db.dates);
            db.students.RemoveRange(db.students);
            db.SaveChanges();
        }
    }
}
