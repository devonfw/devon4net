using Devon4Net.Infrastructure.Log;
using Devon4Net.Infrastructure.MediatR.Common;
using Devon4Net.Infrastructure.MediatR.Domain.ServiceInterfaces;
using MediatR;

namespace Devon4Net.Infrastructure.MediatR.Handler
{
    public class MediatRHandler : IMediatRHandler
    {
        private IMediator Mediator { get; set; }
        private IMediatRBackupService MediatRBackupService { get; set; }
        private IMediatRBackupLiteDbService MediatRBackupLiteDbService { get; set; }

        public MediatRHandler(IMediator mediator)
        {
            BasicSetup(mediator, null, null);
        }

        public MediatRHandler(IMediator mediator, IMediatRBackupService mediatRBackupService, IMediatRBackupLiteDbService mediatRBackupLiteDbService)
        {
            BasicSetup(mediator, mediatRBackupService, mediatRBackupLiteDbService);
        }

        public MediatRHandler(IMediator mediator, IMediatRBackupLiteDbService mediatRBackupLiteDbService)
        {
            BasicSetup(mediator, null, mediatRBackupLiteDbService);
        }

        public MediatRHandler(IMediator mediator, IMediatRBackupService mediatRBackupService)
        {
            BasicSetup(mediator, mediatRBackupService, null);
        }

        public Task<TResult> QueryAsync<TResult>(ActionBase<TResult> query) where TResult : class
        {
            Devon4NetLogger.Debug("Sending the query");
            return SendAsync(query);
        }

        public Task<TResult> CommandAsync<TResult>(ActionBase<TResult> query) where TResult : class
        {
            Devon4NetLogger.Debug("Sending the command");
            return SendAsync(query);
        }

        private async Task<TResult> SendAsync<TResult>(ActionBase<TResult> query) where TResult : class
        {
            await BackUpMessage(query).ConfigureAwait(false);
            return await Mediator.Send(query).ConfigureAwait(false);
        }

        private void BasicSetup(IMediator mediator, IMediatRBackupService mediatRBackupService, IMediatRBackupLiteDbService mediatRBackupLiteDbService)
        {
            Mediator = mediator ?? throw new ArgumentException("Please register the meditaror object in your DI container");
            MediatRBackupService = mediatRBackupService;
            MediatRBackupLiteDbService = mediatRBackupLiteDbService;
        }

        private async Task BackUpMessage<TResult>(ActionBase<TResult> command, MediatrActions queueAction = MediatrActions.Sent, bool increaseRetryCounter = false, string additionalData = null, string errorData = null) where TResult : class
        {
            try
            {
                MediatRBackupLiteDbService?.CreateMessageBackup(command, queueAction, increaseRetryCounter, additionalData, errorData);

                if (MediatRBackupService?.UseExternalDatabase == true)
                {
                    await MediatRBackupService.CreateMessageBackup(command, queueAction, increaseRetryCounter, additionalData, errorData).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error($"Error making the backup of the MediatR action base. {ex.Message} {ex.InnerException}");
                Devon4NetLogger.Error(ex);
                throw;
            }
        }
    }
}
