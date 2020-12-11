using System;

namespace Devon4Net.Infrastructure.MediatR.Domain.Entities
{
    public partial class MediatRBackup
    {
        public Guid Id { get; set; }
        public Guid InternalMessageIdentifier { get; set; }
        public string MessageType { get; set; }
        public DateTime TimeStampUTC { get; set; }
        public string MessageContent { get; set; }
        public string AdditionalData { get; set; }
        public string LogContent { get; set; }
        public bool IsError { get; set; }
        public string Error { get; set; }
        public int Retries { get; set; }
        public string Action { get; set; }
    }
}
