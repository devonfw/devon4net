using Devon4Net.Infrastructure.Common.Constants;
using Devon4Net.Infrastructure.Common.Handlers;
using Devon4Net.Infrastructure.LiteDb.Repository;
using Devon4Net.Infrastructure.MediatR.Data.Service;
using Devon4Net.Infrastructure.MediatR.Domain.Entities;
using Devon4Net.Infrastructure.MediatR.Domain.ServiceInterfaces;
using Devon4Net.Infrastructure.MediatR.Handler;
using Devon4Net.Infrastructure.MediatR.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Devon4Net.Infrastructure.Common;
using Devon4Net.Infrastructure.LiteDb.LiteDb;
using Devon4Net.Infrastructure.MediatR.Domain.Database;

namespace Devon4Net.Infrastructure.MediatR
{
    public static class MediatRConfiguration
    {
        public static void SetupMediatR(this IServiceCollection services, IConfiguration configuration)
        {
            var mediatROptions = services.GetTypedOptions<MediatROptions>(configuration, OptionsDefinition.MediatR);

            if (mediatROptions?.EnableMediatR != true) return;
            services.AddTransient(typeof(IMediatRHandler), typeof(MediatRHandler));
            if (mediatROptions.Backup?.UseLocalBackup != true) return;
            SetupMediatRBackupLocalDatabase(ref services);
        }

        private static void SetupMediatRBackupLocalDatabase(ref IServiceCollection services)
        {
            services.AddTransient(typeof(ILiteDbRepository<MediatRBackup>), typeof(LiteDbRepository<MediatRBackup>));
            services.AddTransient(typeof(IMediatRBackupService), typeof(MediatRBackupService));
            services.AddTransient(typeof(IMediatRHandler), typeof(MediatRHandler));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


            Devon4NetLogger.Information("Please setup your database in order to have the RabbitMq messaging backup feature");
            Devon4NetLogger.Information("RabbitMq messaging backup feature is going to be used via LiteDb");

            services.AddSingleton<ILiteDbContext, MediatRBackupLiteDbContext>();
            services.AddTransient(typeof(IMediatRBackupLiteDbService), typeof(MediatRBackupLiteDbService));
        }
    }
}
