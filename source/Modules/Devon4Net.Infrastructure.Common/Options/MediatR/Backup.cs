namespace Devon4Net.Infrastructure.Common.Options.MediatR
{
    public class Backup
    {
        public bool UseLocalBackup { get; set; }
        public string DatabaseName { get; set; }
    }
}