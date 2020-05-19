using System;
using Devon4Net.Infrastructure.RabbitMQ.Domain.ServiceInterfaces;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using IServiceCollection = Microsoft.Extensions.DependencyInjection.IServiceCollection;

namespace Devon4Net.Infrastructure.RabbitMQ.Common
{
    public static class ServiceConfiguration
    {
        public static void AddRabbitMqHandler<T>(this IServiceCollection services, bool subscribeToQueue) where T : class
        {
            var sp = services.BuildServiceProvider();
            var bus = sp.GetService<IBus>();
            var repoLite = sp.GetService<IRabbitMqBackupLiteDbService>();
            var repo = sp.GetService<IRabbitMqBackupService>();

            var obj = (T)Activator.CreateInstance(typeof(T), services, bus, repo, repoLite, subscribeToQueue);

            services.AddSingleton(obj);
        }
    }
}
