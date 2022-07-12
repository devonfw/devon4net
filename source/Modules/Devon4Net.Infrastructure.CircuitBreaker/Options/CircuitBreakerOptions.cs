namespace Devon4Net.Infrastructure.CircuitBreaker.Options
{
    public class CircuitBreakerOptions
    {
        public bool CheckCertificate { get; set; }
        public List<Endpoint> Endpoints { get; set; }
    }
}