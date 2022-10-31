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
        public int Id { get; set; }
        public List<Const.Subject>? Subjects { get; set; }
        
        public Const.Position Position { get; set; }
        
        public int Experience { get; set; } = 0;

        public List<Dates> dates { get; set; }

    }
}
