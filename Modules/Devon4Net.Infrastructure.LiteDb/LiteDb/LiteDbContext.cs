using Devon4Net.Infrastructure.Common;
using Devon4Net.Infrastructure.Common.Options.LiteDb;
using LiteDB;
using Microsoft.Extensions.Options;

namespace Devon4Net.Infrastructure.LiteDb.LiteDb
{
    public class LiteDbContext : ILiteDbContext
    {
        public LiteDatabase Database { get; }

        public LiteDbContext(IOptions<LiteDbOptions> options)
        {
            var path = FileOperations.GetFileFullPath(options.Value.DatabaseLocation);
            var connection = string.IsNullOrEmpty(path) ? "Filename=devon4netMessageBackup.db;Connection=shared" : $"Filename={path};Connection=shared";

            Database = new LiteDatabase(connection);
        }
    }
}
