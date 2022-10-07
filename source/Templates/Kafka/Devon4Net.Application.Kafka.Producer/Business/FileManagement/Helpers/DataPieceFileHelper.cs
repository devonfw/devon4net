using Devon4Net.Application.Kafka.Producer.Business.FileManagement.Dto;

namespace Devon4Net.Application.Kafka.Producer.Business.FileManagement.Helpers
{
    public static class DataPieceFileHelper
    {
        public static List<DataPieceDto<byte[]>> GetDataPieces(IFormFile file, int byteChunks = 2048)
        {
            var totalParts = (int) Math.Ceiling((double) file.Length / byteChunks);
            var fileExtension = Path.GetExtension(file.FileName);
            var dataPieces = new List<DataPieceDto<byte[]>>();
            var guid = Guid.NewGuid();

            using (Stream jsonReader = file.OpenReadStream())
            {
                byte[] buffer = new byte[byteChunks];
                int position = 1;
                int bytesRead;

                while ((bytesRead = jsonReader.Read(buffer, 0, byteChunks)) > 0)
                {
                    var piece = new DataPieceDto<byte[]>()
                    {
                        Guid = guid,
                        Data = bytesRead < byteChunks
                            ? buffer.Take(bytesRead).ToArray()
                            : buffer,
                        PieceOffset = byteChunks,
                        FileName = file.FileName,
                        FileExtension = fileExtension,
                        Position = position++,
                        TotalParts = totalParts
                    };
                    dataPieces.Add(piece);
                    buffer = new byte[byteChunks];
                }
            }

            return dataPieces;
        }
    }
}
