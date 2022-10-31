using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Kursova.Const;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Kursova.Entity_Framework
{
    internal class EntityLogicContext : DbContext
    {
        public DbSet<Student> students => Set<Student>();
        public DbSet<Professor> professors => Set<Professor>();
        public DbSet<Dates> dates => Set<Dates>();
        public DbSet<Course> courses => Set<Course>();
        public DbSet<Group> groups => Set<Group>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Professor>()
                .Property(e => e.Subjects)
                .HasConversion(
                    v => string.Join(",", v.Select(e => e.ToString("D")).ToArray()),
                    v => v.Split(new[] { ',' })
                      .Select(e => Enum.Parse(typeof(Subject), e))
                      .Cast<Subject>()
                      .ToList()
                );

           
           

            modelBuilder
                .Entity<Course>()
                .Property(e => e.Subjects)
                .HasConversion(
                    v => string.Join(",", v.Select(e => e.ToString("D")).ToArray()),
                    v => v.Split(new[] { ',' })
                      .Select(e => Enum.Parse(typeof(Subject), e))
                      .Cast<Subject>()
                      .ToList()
                );

            
        }
        public EntityLogicContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database20.db");
        }
    }
}
