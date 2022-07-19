namespace Devon4Net.Infrastructure.Logger.Options
{
    public class LogOptions
    {
        public bool UseLogFile { get; set; }
        public bool UseSQLiteDb { get; set; }
        public bool UseGraylog { get; set; }
        public bool UseAOPTrace { get; set; }
        public Loglevel LogLevel { get; set; }
        public string SqliteDatabase { get; set; }
        public string LogFile { get; set; }
        public string SeqLogServerHost { get; set; }
        public GraylogOptions GrayLog { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
        public string MicrosoftAspNetCore { get; set; }
    }

}
