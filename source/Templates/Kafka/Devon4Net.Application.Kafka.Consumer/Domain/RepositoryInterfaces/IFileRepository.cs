using Devon4Net.Application.Kafka.Consumer.Domain.Entities;
using Devon4Net.Domain.UnitOfWork.Repository;

namespace Devon4Net.Application.Kafka.Consumer.Domain.RepositoryInterfaces
{
    public interface IFileRepository : IRepository<DataPiece<byte[]>>
    {
        IList<DataPiece<byte[]>> GetPiecesByFileGuid(string guid);
        bool IsFileComplete(string guid);
        IList<string> GetDistinctFileGuids();
        bool DeleteByGuid(string guid);
    }
}
