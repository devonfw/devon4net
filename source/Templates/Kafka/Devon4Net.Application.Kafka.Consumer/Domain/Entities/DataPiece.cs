namespace Devon4Net.Application.Kafka.Consumer.Domain.Entities
{
    public class DataPiece<T> where T : class
    {
        public int Id { get; set; } 
        public Guid Guid { get; set; }
        public string FileName { get; set; }
        public int TotalParts { get; set; }
        public string FileExtension { get; set; }
        public int PieceOffset { get; set; }
        public int Position { get; set; }
        public T Data { get; set; }
    }
}
