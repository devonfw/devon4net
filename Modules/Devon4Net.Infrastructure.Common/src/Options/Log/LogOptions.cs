using System;
using System.Collections.Generic;
using System.Text;

namespace Devon4Net.Infrastructure.Common.Options.Log
{
    public class LogOptions
    {
        public bool UseAOPTrace { get; set; }
        public string LogLevel { get; set; }
        public string SqliteDatabase { get; set; }
        public string LogFile { get; set; }
        public string SeqLogServerHost { get; set; }
        public GraylogOptions GrayLog { get; set; }
    }
}
