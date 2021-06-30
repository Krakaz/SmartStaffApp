using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repo.Models
{
    public class RepoContext : DbContext
    {

        public DbSet<City> Cities { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Value> Values { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<NotificationEmail> NotificationEmails { get; set; }

        public RepoContext(DbContextOptions<RepoContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
