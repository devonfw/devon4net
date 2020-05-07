using Devon4Net.Infrastructure.RabbitMQ.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Devon4Net.Infrastructure.RabbitMQ.Domain.Database
{
    public class RabbitMqBackupContext : DbContext
    {
        public RabbitMqBackupContext()
        {
        }

        public RabbitMqBackupContext(DbContextOptions<RabbitMqBackupContext> options)
            : base(options)
        {
        }

        public virtual DbSet<RabbitBackup> RabbitBackup { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RabbitBackup>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.MessageType).IsRequired();
            });
        }
    }
}
