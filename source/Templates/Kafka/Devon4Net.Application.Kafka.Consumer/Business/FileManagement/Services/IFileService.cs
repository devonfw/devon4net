using Devon4Net.Application.Kafka.Consumer.Business.FileManagement.Dto;

namespace Devon4Net.Application.Kafka.Consumer.Business.FileManagement.Services
{
    public interface IFileService
    {
        public IList<string> GetDistinctFileGuids();
        public IList<DataPieceDto<byte[]>> GetPiecesByFileGuid(string guid);
        public bool IsFileComplete(string guid);
        public DataPieceDto<byte[]> CreateFile(DataPieceDto<byte[]> dataPiece);
        public bool DeleteFileByGuid(string guid);
    }
}