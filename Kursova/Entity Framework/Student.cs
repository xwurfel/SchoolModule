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
        public int courseNumber { get; set; } = 1;
        public int groupNumber { get; set; } = 1;
        public Const.Enums.Subject subjects { get; set; } = Const.Enums.Subject.Math;

    }
}
