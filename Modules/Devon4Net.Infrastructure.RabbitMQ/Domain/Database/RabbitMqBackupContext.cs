using Microsoft.EntityFrameworkCore;

namespace Devon4Net.Infrastructure.RabbitMQ.Domain.Database
{
    public partial class RabbitMqBackupContext : DbContext
    {
        public RabbitMqBackupContext()
        {
        }

        public RabbitMqBackupContext(DbContextOptions<RabbitMqBackupContext> options)
            : base(options)
        {
        }

        public virtual DbSet<RabbitBackup> RabbitBackup { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RabbitBackup>(entity =>
            {
                entity.HasKey(e => e.InternalMessageIdentifier);

                entity.Property(e => e.MessageType).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
