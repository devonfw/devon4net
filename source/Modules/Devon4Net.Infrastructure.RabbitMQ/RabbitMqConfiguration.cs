using System.Net.Security;
using Devon4Net.Infrastructure.Common.Handlers;
using Devon4Net.Infrastructure.Common.Helpers;
using Devon4Net.Infrastructure.Common.Helpers.Interfaces;
using Devon4Net.Infrastructure.Common.IO;
using Devon4Net.Infrastructure.LiteDb.LiteDb;
using Devon4Net.Infrastructure.LiteDb.Repository;
using Devon4Net.Infrastructure.Common;
using Devon4Net.Infrastructure.RabbitMQ.Data.Service;
using Devon4Net.Infrastructure.RabbitMQ.Domain.Database;
using Devon4Net.Infrastructure.RabbitMQ.Domain.Entities;
using Devon4Net.Infrastructure.RabbitMQ.Domain.ServiceInterfaces;
using Devon4Net.Infrastructure.RabbitMQ.Options;
using EasyNetQ;
using EasyNetQ.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Devon4Net.Infrastructure.RabbitMQ
{
    public static class RabbitMqConfiguration
    {
        public static void SetupRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqOptions = services.GetTypedOptions<RabbitMqOptions>(configuration, "RabbitMq");
            if (rabbitMqOptions?.EnableRabbitMq != true || rabbitMqOptions.Hosts?.Any() != true) return;

            if (rabbitMqOptions.Hosts?.Any() != true)
            {
                return;
            }

            try
            {
                ConfigureRabbitMqGenericDependencyInjection(services);
                SetupRabbitMqBackupLocalDatabase(services, rabbitMqOptions);

                var prefetchCount = (ushort)(rabbitMqOptions.PrefetchCount != null ? (ushort)rabbitMqOptions.PrefetchCount.Value : 0);
                var timeOut = rabbitMqOptions.Timeout != null ? TimeSpan.FromSeconds(rabbitMqOptions.Timeout.Value) : default;
                var requestedHeartbeat = (rabbitMqOptions.RequestedHeartbeat != null ? TimeSpan.FromSeconds(rabbitMqOptions.RequestedHeartbeat.Value) : default);

                var connection = new ConnectionConfiguration
                {
                    Hosts = rabbitMqOptions.Hosts.ConvertAll(GetHostConfiguration),
                    PersistentMessages = rabbitMqOptions.PersistentMessages,
                    PublisherConfirms = rabbitMqOptions.PublisherConfirms,
                    Product = rabbitMqOptions.Product,
                    VirtualHost = string.IsNullOrEmpty(rabbitMqOptions.VirtualHost) ? "/" : rabbitMqOptions.VirtualHost,
                    Platform = rabbitMqOptions.Platform,
                    PrefetchCount = prefetchCount > 0 ? prefetchCount : (ushort)50,
                    Timeout = timeOut,
                    UserName = rabbitMqOptions.UserName,
                    Password = rabbitMqOptions.Password,
                    RequestedHeartbeat = requestedHeartbeat,
                };

                services.AddSingleton(RabbitHutch.CreateBus(connection, serviceRegister => serviceRegister.Register(_ => Log.Logger)));
            }
            catch (ArgumentNullException ex) { Devon4NetLogger.Error(ex); }
            catch (EasyNetQException ex) { Devon4NetLogger.Error(ex); }
            catch (ArgumentException ex) { Devon4NetLogger.Error(ex); }
            catch (PathTooLongException ex ) { Devon4NetLogger.Error(ex); }
        }

        public static void AddRabbitMqHandler<T>(this IServiceCollection services, bool subscribeToQueue) where T : class
        {
            var memberInfo = typeof(T).BaseType;

            if (memberInfo?.Name.Contains("RabbitMqHandler") == false)
            {
                throw new ArgumentException($"The provided type {typeof(T).FullName} does not inherit from RabbitMqHandler");
            }

            using var sp = services.BuildServiceProvider();
            var bus = sp.GetService<IBus>();
            var repoLite = sp.GetService<IRabbitMqBackupLiteDbService>();
            var repo = sp.GetService<IRabbitMqBackupService>();

            var obj = (T)Activator.CreateInstance(typeof(T), services, bus, repo, repoLite, subscribeToQueue);

            services.AddSingleton(obj);
        }

        private static void ConfigureRabbitMqGenericDependencyInjection(IServiceCollection services)
        {
            services.AddTransient(typeof(IJsonHelper), typeof(JsonHelper));
            services.AddTransient(typeof(ILiteDbRepository<RabbitBackup>), typeof(LiteDbRepository<RabbitBackup>));
            services.AddTransient(typeof(IRabbitMqBackupService), typeof(RabbitMqBackupService));
        }

        private static void SetupRabbitMqBackupLocalDatabase(IServiceCollection services, RabbitMqOptions rabbitMqOptions)
        {
            Devon4NetLogger.Information("Please setup your database in order to have the RabbitMq messaging backup feature");
            if (rabbitMqOptions.Backup?.UseLocalBackup != true) return;
            Devon4NetLogger.Information("RabbitMq messaging backup feature is going to be used via LiteDb");

            services.AddSingleton<ILiteDbContext, RabbitMqBackupLiteDbContext>();
            services.AddTransient(typeof(IRabbitMqBackupLiteDbService), typeof(RabbitMqBackupLiteDbService));
        }

        private static HostConfiguration GetHostConfiguration(HostDefinition host)
        {
            var port = (ushort)(host.Port != null ? (ushort)host.Port.Value : 0);
            var hostConfiguration = new HostConfiguration { Host = host.Host, Port = port};

            if (port > 0) hostConfiguration.Port = port;
            _ = Enum.TryParse(host.SslPolicyErrors, out SslPolicyErrors sslPolicyErrors);

            if (!host.Ssl) return hostConfiguration;

            hostConfiguration.Ssl.CertPassphrase = host.SslCertPassPhrase;
            hostConfiguration.Ssl.AcceptablePolicyErrors = sslPolicyErrors;
            hostConfiguration.Ssl.ServerName = host.SslServerName;
            hostConfiguration.Ssl.CertPath = FileOperations.GetFileFullPath(host.SslCertPath);
            hostConfiguration.Ssl.Enabled = host.Ssl;

            return hostConfiguration;
        }
    }
}