namespace Devon4Net.Infrastructure.MediatR.Options
{
    public class Backup
    {
        public bool UseLocalBackup { get; set; }
        public string DatabaseName { get; set; }
    }
}