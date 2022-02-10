namespace Devon4Net.Infrastructure.Common.Options.Swagger
{
    public class SwaggerOptions
    {
        public string Version { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Terms { get; set; }
        public Contact Contact { get; set; }
        public License License { get; set; }
        public Endpoint Endpoint { get; set; }
    }
}