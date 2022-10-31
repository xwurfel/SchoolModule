using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova.Entity_Framework
{
    internal class Group
    {
        public int Id { get; set; }
        
        public Course Course { get; set; }

        public string? Name { get; set; }

        public List<Dates>? dates { get; set; }

        public List<Student>? students { get; set; }

    }
}
