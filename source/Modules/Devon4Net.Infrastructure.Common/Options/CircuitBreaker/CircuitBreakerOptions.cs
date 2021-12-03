namespace Devon4Net.Infrastructure.Common.Options.CircuitBreaker
{
    public class CircuitBreakerOptions
    {
        public bool CheckCertificate { get; set; }
        public List<Endpoint> Endpoints { get; set; }
    }
}