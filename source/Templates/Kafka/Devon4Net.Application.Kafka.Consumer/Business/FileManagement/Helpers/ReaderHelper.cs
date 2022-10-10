using Devon4Net.Application.Kafka.Consumer.Business.FileManagement.Dto;

namespace Devon4Net.Application.Kafka.Consumer.Business.FileManagement.Helpers
{
    public static class ReaderHelper
    {
        /// <summary>
        /// Creates a file in the specified directory given an unordered list of data pieces. 
        /// </summary>
        /// <param name="pieces">List of pieces that form the file.</param>
        /// <param name="directoryPath">Directory where the output file will be created</param>
        /// <param name="fileName">Name of the file</param>
        /// <returns></returns>
        public static async Task ReadPiecesAndWriteToFile(IEnumerable<DataPieceDto<byte[]>> pieces, string directoryPath, string fileName = "output")
        {
            pieces = pieces.OrderBy(o => o.Position);
            using (var fileStream = new FileStream(@$"{directoryPath}\{fileName}{pieces.First().FileExtension}", FileMode.Create))
            {
                foreach (var piece in pieces)
                {
                    await fileStream.WriteAsync(piece.Data, 0, piece.Data.Length);
                }
            }
        }
    }
}
