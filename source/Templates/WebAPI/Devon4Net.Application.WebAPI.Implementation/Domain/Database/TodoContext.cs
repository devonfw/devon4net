using Devon4Net.Application.WebAPI.Implementation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Devon4Net.Application.WebAPI.Implementation.Domain.Database
{
    /// <summary>
    /// Context definition
    /// </summary>
    public class TodoContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Dbset
        /// </summary>
        public virtual DbSet<Todos> Todos { get; set; }

        /// <summary>
        /// Model rules definition
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todos>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255);
            });
        }
    }
}