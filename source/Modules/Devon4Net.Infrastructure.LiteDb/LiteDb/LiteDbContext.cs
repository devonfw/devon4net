using Devon4Net.Infrastructure.Common.Common.IO;
using Devon4Net.Infrastructure.LiteDb.Options;
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
            var connection = string.IsNullOrEmpty(path) ? $"Filename={options.Value.DatabaseLocation};Connection=shared" : $"Filename={path};Connection=shared";
            Database = new LiteDatabase(connection);
        }
    }
}
