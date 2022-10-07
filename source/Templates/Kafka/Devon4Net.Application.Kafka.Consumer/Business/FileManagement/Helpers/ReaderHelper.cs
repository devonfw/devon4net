using Devon4Net.Application.Kafka.Consumer.Business.FileManagement.Dto;

namespace Devon4Net.Application.Kafka.Consumer.Business.FileManagement.Helpers
{
    public static class ReaderHelper
    {
        public static async Task ReadPiecesAndWriteToFile(IEnumerable<DataPieceDto<byte[]>> pieces, string directoryPath, string fileName = "output", int byteChunks = 2048)
        {
            using (var fileStream = new FileStream(@$"{directoryPath}\{fileName}{pieces.First().FileExtension}", FileMode.Create))
            {
                foreach (var piece in pieces)
                {
                    await fileStream.WriteAsync(piece.Data, 0, piece.Data.Length);
                }
            }
        }

        private static async void ReadPiecesAndWriteToFileAsync(IEnumerable<DataPieceDto<byte[]>> pieces, string directoryPath, string fileName = "output", int byteChunks = 2048)
        {
            var taskList = new List<Task>();
            foreach (var piece in pieces)
            {
                //taskList.Append(ReadPieceAndWriteToFile(piece, directoryPath));
            }
            await Task.WhenAll(taskList);
        }
    }
}
