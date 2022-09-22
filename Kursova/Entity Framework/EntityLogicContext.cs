using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kursova.Const;
using Microsoft.EntityFrameworkCore;

namespace Kursova.Entity_Framework
{
    internal class EntityLogicContext : DbContext
    {
        public DbSet<Student> students => Set<Student>();
        public DbSet<Professor> professors => Set<Professor>();
        public DbSet<LessonsDate> dates => Set<LessonsDate>();

        //public DbSet<Enums.Subject> subjects => Set<Enums.Subject>();
        public EntityLogicContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database1.db");
        }
    }
}
