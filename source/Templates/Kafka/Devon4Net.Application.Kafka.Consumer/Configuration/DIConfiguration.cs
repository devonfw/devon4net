using Devon4Net.Application.Kafka.Consumer.Domain.Database;
using Devon4Net.Domain.UnitOfWork.Common;
using Devon4Net.Domain.UnitOfWork.Enums;

namespace Devon4Net.Application.Kafka.Consumer.Configuration
{
    public static class DIConfiguration
    {
        public static void SetupDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            SetupDatabase(services, configuration);
        }

        private static void SetupDatabase(IServiceCollection services, IConfiguration configuration)
        {
            services.SetupDatabase<FileContext>(configuration, "FileDb", DatabaseType.InMemory).ConfigureAwait(false);
        }
    }
}
