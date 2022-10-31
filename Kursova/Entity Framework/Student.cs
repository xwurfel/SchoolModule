﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova.Entity_Framework
{
    internal class Student: Person
    {
        public int Id { get; set; }
        
        public Group Group { get; set; }
            
    }
}
