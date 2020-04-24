using System;
using System.IO;
using System.Linq;
using System.Net.Security;
using Devon4Net.Infrastructure.Common.Options.RabbitMq;
using Devon4Net.Infrastructure.Log;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Devon4Net.Application.WebAPI.Configuration
{
    public static class RabbitMqConfiguration
    {
        public static void SetupRabbitMq(this IServiceCollection services, RabbitMQOptions rabbitMqOptions)
        {
            if (rabbitMqOptions?.Hosts == null || !rabbitMqOptions.Hosts.Any())
            {
                return;
            }

            try
            {
                var prefetchCount = (ushort)(rabbitMqOptions.PrefetchCount != null ? (ushort)rabbitMqOptions.PrefetchCount.Value : 0);
                var timeOut = (ushort)(rabbitMqOptions.Timeout != null ? (ushort)rabbitMqOptions.Timeout.Value : 0);
                var requestedHeartbeat = (ushort)(rabbitMqOptions.RequestedHeartbeat != null ? (ushort)rabbitMqOptions.RequestedHeartbeat.Value : 0);

                var connection = new ConnectionConfiguration
                {
                    Hosts = rabbitMqOptions.Hosts.Select(GetHostConfiguration).ToList(),
                    PersistentMessages = rabbitMqOptions.PersistentMessages,
                    PublisherConfirms = rabbitMqOptions.PublisherConfirms,
                    Product = rabbitMqOptions.Product,
                    VirtualHost = string.IsNullOrEmpty(rabbitMqOptions.VirtualHost) ? "/" : rabbitMqOptions.VirtualHost,
                    Platform = rabbitMqOptions.Platform,
                    PrefetchCount = prefetchCount > 0 ? prefetchCount : (ushort)50,
                    Timeout = timeOut > 0 ? timeOut : (ushort)10,
                    UserName = rabbitMqOptions.UserName,
                    Password = rabbitMqOptions.Password,
                    RequestedHeartbeat = requestedHeartbeat > 0 ? requestedHeartbeat : (ushort)10,
                };

                connection.Validate();

                services.AddSingleton<IBus>(RabbitHutch.CreateBus(connection, serviceRegister => serviceRegister.Register(serviceProvider => Log.Logger)));
            }
            catch (ArgumentNullException ex) { Devon4NetLogger.Error(ex); }
            catch (EasyNetQException ex) { Devon4NetLogger.Error(ex); }
            catch (ArgumentException ex) { Devon4NetLogger.Error(ex); }
            catch (PathTooLongException ex ) { Devon4NetLogger.Error(ex); }
        }

        private static HostConfiguration GetHostConfiguration(HostDefnition host)
        {
            var hostConfiguration = new HostConfiguration { Host = host.Host };
            var port = (ushort) (host.Port != null ? (ushort) host.Port.Value : 0);
            
            if (port > 0) hostConfiguration.Port = port;
            Enum.TryParse(host.SslPolicyErrors, out SslPolicyErrors sslPolicyErrors);

            if (!host.Ssl) return hostConfiguration;

            hostConfiguration.Ssl.CertPassphrase = host.SslCertPassPhrase;
            hostConfiguration.Ssl.AcceptablePolicyErrors = sslPolicyErrors;
            hostConfiguration.Ssl.ServerName = host.SslServerName;
            hostConfiguration.Ssl.CertPath = host.SslCertPath;
            hostConfiguration.Ssl.Enabled = host.Ssl;

            return hostConfiguration;
        }
    }
}