using Devon4Net.Application.Kafka.Consumer.Business.FileManagement.Dto;
using Devon4Net.Application.Kafka.Consumer.Domain.Entities;

namespace Devon4Net.Application.Kafka.Consumer.Business.FileManagement.Converters
{
    public static class DataPieceConverter
    {
        public static DataPieceDto<byte[]> ModelToDto(DataPiece<byte[]> item)
        {
            if (item == null) return new DataPieceDto<byte[]>();

            return new DataPieceDto<byte[]>
            {
                Guid = item.Guid,
                FileName = item.FileName,
                TotalParts = item.TotalParts,
                FileExtension = item.FileExtension,
                PieceOffset = item.PieceOffset,
                Position = item.Position,
                Data = item.Data
            };
        }

        public static IList<DataPieceDto<byte[]>> ModelToDto(IList<DataPiece<byte[]>> entityList)
        {
            List<DataPieceDto<byte[]>> dtoList = new List<DataPieceDto<byte[]>>();
            foreach (var item in entityList)
            {
                var dto = ModelToDto(item);
                dtoList.Add(dto);
            }
            return dtoList;
        }

        public static DataPiece<byte[]> DtoToModel(DataPieceDto<byte[]> item)
        {
            if (item == null) return new DataPiece<byte[]>();

            return new DataPiece<byte[]>
            {
                Guid = item.Guid,
                FileName = item.FileName,
                TotalParts = item.TotalParts,
                FileExtension = item.FileExtension,
                PieceOffset = item.PieceOffset,
                Position = item.Position,
                Data = item.Data
            };
        }
    }
}
