namespace Devon4Net.Infrastructure.CyberArk.Options
{
    public class CyberArkOptions
    {
        public bool EnableCyberArk { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CircuitBreakerName { get; set; }
    }
}
