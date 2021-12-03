using Devon4Net.Application.WebAPI.Implementation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Devon4Net.Application.WebAPI.Implementation.Domain.Database
{
    /// <summary>
    /// Employee database context definition
    /// </summary>
    public class EmployeeContext : DbContext
    {
        /// <summary>
        /// Employee context definition
        /// </summary>
        /// <param name="options"></param>
        public EmployeeContext(DbContextOptions<EmployeeContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Dbset
        /// </summary>
        public virtual DbSet<Employee> Employee { get; set; }

        /// <summary>
        /// Any extra configuration should be here
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        /// <summary>
        /// Model rules definition
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasMaxLength(255);
            });
        }
    }
}