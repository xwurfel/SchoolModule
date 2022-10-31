using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova.Entity_Framework
{
    internal class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Const.Subject>? Subjects { get; set; }

        public List<Group> groups { get; set; }
    }
}
