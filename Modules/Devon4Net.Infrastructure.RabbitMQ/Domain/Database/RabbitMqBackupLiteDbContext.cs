using Devon4Net.Infrastructure.Common;
using Devon4Net.Infrastructure.Common.Options.RabbitMq;
using LiteDB;
using Microsoft.Extensions.Options;

namespace Devon4Net.Infrastructure.LiteDb.LiteDb
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
            Database = new LiteDatabase(string.IsNullOrEmpty(path) ? "RabbitMqBackup.db" : path);
        }
    }
}
