using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UserApi.Entities
{
    public class APIDbContext : DbContext
    {
        private string _connectionString =
            "Server=DESKTOP-MQ98A98;Database=IntiveDB2;Trusted_Connection=True;";
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Address { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(r => r.FirstName)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(b => b.DateOfBirth)
                .HasPrecision(0);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
