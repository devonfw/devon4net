namespace Devon4Net.Infrastructure.Logger.Common.Options
{
    public class LogOptions
    {
        public bool UseLogFile { get; set; }
        public bool UseSqLiteDb { get; set; }
        public bool UseGraylog { get; set; }
        public bool UseAopTrace { get; set; }
        public Loglevel LogLevel { get; set; }
        public string SqliteDatabase { get; set; }
        public string LogFile { get; set; }
        public string SeqLogServerHost { get; set; }
        public GraylogOptions GrayLog { get; set; }
    }
}
