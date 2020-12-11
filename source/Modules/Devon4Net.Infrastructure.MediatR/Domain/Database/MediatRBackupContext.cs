using Devon4Net.Infrastructure.MediatR.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Devon4Net.Infrastructure.MediatR.Domain.Database
{
    public class MediatRBackupContext : DbContext
    {
        public MediatRBackupContext()
        {
        }

        public MediatRBackupContext(DbContextOptions<MediatRBackupContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MediatRBackup> MediatRBackup { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MediatRBackup>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.MessageType).IsRequired();
            });
        }
    }
}
