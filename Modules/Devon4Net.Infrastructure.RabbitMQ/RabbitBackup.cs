using System;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.RabbitMQ
{
    public partial class RabbitBackup
    {
        public string InternalMessageIdentifier { get; set; }
        public string MessageType { get; set; }
        public string Timestamp { get; set; }
        public string MessageContent { get; set; }
        public string AdditionalData { get; set; }
        public string LogContent { get; set; }
        public long? IsError { get; set; }
        public string Error { get; set; }
    }
}
