using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova.Entity_Framework
{
    internal class Professor: Person
    {
        public List<Const.Subject> Subjects { get; set; }
        public List<int> Groups { get; set; }
        public Const.Position Position { get; set; }
        public LessonsDate? Schedule { get; set; } = null;
        public int Experience { get; set; } = 0;

    }
}
