using Microsoft.EntityFrameworkCore;
using PEOTest.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEOTest.DAL
{
    public class EFDbContext : DbContext
    {
        public EFDbContext(DbContextOptions<EFDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Subdivision> Subdivision { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<CompEmp> CompEmp { get; set; }
    }
}
