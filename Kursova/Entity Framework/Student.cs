using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova.Entity_Framework
{
    internal class Student: Person
    {
        public int CourseNumber { get; set; } = 1;
        public int GroupNumber { get; set; } = 1;
        public Const.Enums.Subject Subjects { get; set; }

    }
}
