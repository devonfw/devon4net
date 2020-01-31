using System;
using System.Collections.Generic;
using System.Linq;
using Devon4Net.Domain.UnitOfWork.Enums;
using EntityFrameworkCore.Jet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Domain.UnitOfWork.Common
{
    public static class SetupDatabaseConfiguration
    {
        private const int MaxRetryDelay = 30;
        private const int MaxRetryCount = 10;

        public static void SetupDatabase<T>(this IServiceCollection services, IConfiguration configuration, string conectionStringName, DatabaseType databaseType, CosmosConfigurationParams cosmosConfigurationParams = null) where T : DbContext 
        {
            var applicationConnectionStrings = configuration.GetSection("ConnectionStrings").GetChildren();
            if (applicationConnectionStrings == null) throw new ArgumentException("There are no connection strings provided.");
            var connectionString = applicationConnectionStrings.FirstOrDefault(c => c.Key.ToLower() == conectionStringName.ToLower());
            if (connectionString == null || string.IsNullOrEmpty(connectionString.Value)) throw new ArgumentException($"The provided connection string ({conectionStringName}) provided does not exists.");

            switch (databaseType)
            {
                case DatabaseType.SqlServer:
                    services.AddDbContext<T>(options=>options.UseSqlServer(connectionString.Value, sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: MaxRetryCount,
                            maxRetryDelay: TimeSpan.FromSeconds(MaxRetryDelay),
                            errorNumbersToAdd: null);
                    }));
                    break;
                case DatabaseType.InMemory:
                    services.AddDbContext<T>(options => options.UseInMemoryDatabase(connectionString.Value));
                    break;
                case DatabaseType.MySql:
                    services.AddDbContext<T>(options=> options.UseMySql(connectionString.Value, sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: MaxRetryCount,
                            maxRetryDelay: TimeSpan.FromSeconds(MaxRetryDelay),
                            errorNumbersToAdd: new List<int>());
                    }));
                    break;
                case DatabaseType.MariaDb:
                    services.AddDbContext<T>(options => options.UseMySql(connectionString.Value, sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: MaxRetryCount,
                            maxRetryDelay: TimeSpan.FromSeconds(MaxRetryDelay),
                            errorNumbersToAdd: new List<int>());
                    }));
                    break;
                case DatabaseType.Sqlite:
                    services.AddDbContext<T>(options => options.UseSqlite(connectionString.Value));
                    break;
                case DatabaseType.Cosmos:
                    if (cosmosConfigurationParams == null) throw new ArgumentException($"The Cosmos configuration can not be null.");
                    services.AddDbContext<T>(options => options.UseCosmos(cosmosConfigurationParams.Endpoint, cosmosConfigurationParams.Key, cosmosConfigurationParams.DatabaseName));
                    break;
                case DatabaseType.PostgreSQL:
                    services.AddDbContext<T>(options => options.UseNpgsql(connectionString.Value, sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: MaxRetryCount,
                            maxRetryDelay: TimeSpan.FromSeconds(MaxRetryDelay),
                            errorCodesToAdd: new List<string>());
                    }));
                    break;
                case DatabaseType.FireBird:
                    services.AddDbContext<T>(options => options.UseFirebird(connectionString.Value));
                    break;
                case DatabaseType.Oracle:
                    services.AddDbContext<T>(options => options.UseOracle(connectionString.Value));
                    break;
                case DatabaseType.MSAccess:
                    services.AddDbContext<T>(options => options.UseJet(connectionString.Value));
                    break;
                default:
                    throw new ArgumentException("Not provided a database driver");
            }
        }
    }
}
