using LiteDB;

namespace Devon4Net.Infrastructure.LiteDb.LiteDb
{
    public interface ILiteDbContext
    {
        LiteDatabase Database { get; }
    }
}