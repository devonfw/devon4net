using Devon4Net.Application.Kafka.Consumer.Domain.Database;
using Devon4Net.Application.Kafka.Consumer.Domain.Entities;
using Devon4Net.Application.Kafka.Consumer.Domain.RepositoryInterfaces;
using Devon4Net.Domain.UnitOfWork.Repository;

namespace Devon4Net.Application.Kafka.Consumer.Data.Repositories
{
    public class FileRepository : Repository<DataPiece<byte[]>>, IFileRepository
    {
        public FileRepository(FileContext context, bool dbContextBehaviour = false) : base(context, dbContextBehaviour)
        {
        }

        public bool DeleteByGuid(string guid)
        {
            return Delete(o => o.Guid == Guid.Parse(guid)).GetAwaiter().GetResult();
        }

        public IList<string> GetDistinctFileGuids()
        {
            var pieces = Get().GetAwaiter().GetResult();
            return pieces.Select(o => o.Guid.ToString()).Distinct().ToList();
        }

        public IList<DataPiece<byte[]>> GetPiecesByFileGuid(string guid)
        {
            return Get(o => o.Guid == Guid.Parse(guid)).GetAwaiter().GetResult();
        }

        public bool IsFileComplete(string guid)
        {
            var pieces = GetPiecesByFileGuid(guid);
            return pieces.Count == pieces.First().TotalParts;
        }
    }
}
