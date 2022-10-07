using Devon4Net.Application.Kafka.Consumer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Devon4Net.Application.Kafka.Consumer.Domain.Database
{
    public class FileContext : DbContext
    {
        public virtual DbSet<DataPiece<byte[]>> FilePieces { get; set; }

        public FileContext(DbContextOptions<FileContext> options)
            : base(options)
        {
        }

    }
}
