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
            Database = new LiteDatabase(string.IsNullOrEmpty(path) ? "Devon4Net.db" : path);
        }
    }
}
