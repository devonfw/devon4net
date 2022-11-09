﻿using Devon4Net.Infrastructure.Common.Extensions;
using Devon4Net.Infrastructure.Common.Helpers.Interfaces;
using Devon4Net.Infrastructure.Common;
using Devon4Net.Infrastructure.RabbitMQ.Commands;
using Devon4Net.Infrastructure.RabbitMQ.Common;
using Devon4Net.Infrastructure.RabbitMQ.Domain.Database;
using Devon4Net.Infrastructure.RabbitMQ.Domain.Entities;
using Devon4Net.Infrastructure.RabbitMQ.Domain.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Devon4Net.Infrastructure.RabbitMQ.Data.Service
{
    public class RabbitMqBackupService : IRabbitMqBackupService
    {
        public bool UseExternalDatabase { get; set; }
        private IJsonHelper JsonHelper { get; }
        private string ContextConnectionString { get; set; }
        private string ContextProvider { get; set; }

        public RabbitMqBackupService(RabbitMqBackupContext context, IJsonHelper jsonHelper)
        {
            GetContextConnectionAndProvider(context);
            UseExternalDatabase = context != null;
            JsonHelper = jsonHelper;
        }

        public RabbitMqBackupService(IJsonHelper jsonHelper)
        {
            UseExternalDatabase = false;
            JsonHelper = jsonHelper;
        }

        public async Task<RabbitBackup> CreateMessageBackup(Command command, QueueActions action = QueueActions.Sent, bool increaseRetryCounter = false, string additionalData = null, string errorData = null)
        {
            RabbitMqBackupContext ctx = null;

            try
            {
                ctx = CreateContext();

                CheckParamContext(command, ctx);

                var backUp = new RabbitBackup
                {
                    Id = Guid.NewGuid(),
                    InternalMessageIdentifier = command.InternalMessageIdentifier,
                    Retries = increaseRetryCounter ? 1 : 0,
                    AdditionalData = string.IsNullOrEmpty(additionalData) ? string.Empty : additionalData,
                    IsError = false,
                    MessageContent = GetSerializedContent(command),
                    MessageType = command.MessageType,
                    TimeStampUTC = command.Timestamp.ToUniversalTime(),
                    Action = action.ToString(),
                    Error = string.IsNullOrEmpty(errorData) ? string.Empty : errorData
                };

                var result = await ctx.RabbitBackup.AddAsync(backUp).ConfigureAwait(false);
                await ctx.SaveChangesAsync().ConfigureAwait(false);

                return result.Entity;
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
            finally
            {
                if (ctx != null) await ctx.DisposeAsync().ConfigureAwait(false);
            }
        }

        private void CheckParamContext(Command command, RabbitMqBackupContext ctx)
        {
            if (ctx == null)
            {
                throw new ArgumentException("The database provider is not supported to host threads");
            }

            if (!UseExternalDatabase)
            {
                throw new ArgumentException(
                    "Please setup your RabbitMqBackupContext database context to use RabbitMqBackupService");
            }

            if (command?.InternalMessageIdentifier == null || command.InternalMessageIdentifier.IsNullOrEmptyGuid())
            {
                throw new ArgumentException("The provided command  and the command identifier cannot be null");
            }
        }

        /// <summary>
        /// RabbitMq handle the messages with threads
        /// EF Context multi-thread is not allowed
        /// The solution to handle is creating and disposing a database context
        /// Please check https://docs.microsoft.com/en-us/ef/core/miscellaneous/configuring-dbcontext#avoiding-dbcontext-threading-issues
        /// </summary>
        /// <returns>The database context to backup the messages</returns>
        private RabbitMqBackupContext CreateContext()
        {
            const string errorMessage = "The connection string from context cannot be null or the database provider is not supported";
            if (string.IsNullOrEmpty(ContextConnectionString))
            {
                Devon4NetLogger.Error(errorMessage);
            }

            var optionsBuilder = new DbContextOptionsBuilder<RabbitMqBackupContext>();

            switch (ContextProvider)
            {
                case DatabaseConst.SqlServer:
                    optionsBuilder.UseSqlServer(ContextConnectionString);
                    break;
                case DatabaseConst.PostgresSql:
                    optionsBuilder.UseNpgsql(ContextConnectionString);
                    break;
                case DatabaseConst.MySql:
                case DatabaseConst.MySqlPomelo:
                    optionsBuilder.UseMySql(ServerVersion.AutoDetect(ContextConnectionString));
                    break;
                case DatabaseConst.FireBirdSql:
                case DatabaseConst.FireBirdSqlV:
                    optionsBuilder.UseFirebird(ContextConnectionString);
                    break;
                case DatabaseConst.SqlLite:
                    optionsBuilder.UseSqlite(ContextConnectionString);
                    break;
                default:
                    Devon4NetLogger.Error(errorMessage);
                    throw new ArgumentException(errorMessage);
            }

            return new RabbitMqBackupContext(optionsBuilder.Options);
        }

        private string GetSerializedContent(Command command)
        {
            var typedCommand = Convert.ChangeType(command, command.GetType());
            return JsonHelper.Serialize(typedCommand, false);
        }

        private void GetContextConnectionAndProvider(RabbitMqBackupContext context)
        {
            try
            {
                ContextProvider = context.Database.ProviderName;
                ContextConnectionString = context.Database.GetDbConnection().ConnectionString;
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error("Error trying to get the connection string from context.");
                Devon4NetLogger.Error(ex);
            }
        }
    }
}
