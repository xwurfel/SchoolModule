using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova.Entity_Framework
{
    internal class LessonsDate
    {
        
        public int Id { get; set; }
        public DateTime DateTime { get; set; }

        public Const.Subject Subject { get; set; }
        public int Group { get; set; }
        public int Course { get; set; }

        [ForeignKey("ProfessorName")]
        public string ProfessorName { get; set; }
        //public Professor? Professor { get; set; }

    }
}
