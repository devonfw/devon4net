using Devon4Net.Infrastructure.Common;
using Devon4Net.Infrastructure.Common.Options.RabbitMq;
using Devon4Net.Infrastructure.LiteDb.LiteDb;
using LiteDB;
using Microsoft.Extensions.Options;

namespace Devon4Net.Infrastructure.RabbitMQ.Domain.Database
{
    public class RabbitMqBackupLiteDbContext : ILiteDbContext
    {
        public LiteDatabase Database { get; }

        public RabbitMqBackupLiteDbContext(IOptions<RabbitMqOptions> options)
        {
            if (options?.Value?.Backup == null || !options.Value.Backup.UseLocalBackup)
            {
                return;
            }

            var path = FileOperations.GetFileFullPath(options.Value.Backup.DatabaseName);
            var connection = string.IsNullOrEmpty(path) ? "Filename=devon4netMessageBackup.db;Connection=shared;Async=true;" : $"Filename={path};Connection=shared;Async=true;";

            Database = new LiteDatabase(connection);
        }
    }
}
