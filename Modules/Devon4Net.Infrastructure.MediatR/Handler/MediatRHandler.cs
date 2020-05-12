using System;
using System.Threading.Tasks;
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

        public async Task<TResult> QueryAsync<TResult>(ActionBase<TResult> query) where TResult : class
        {
            await BackUpMessage(query);
            return await Mediator.Send(query);
        }

        public async Task<TResult> CommandAsync<TResult>(ActionBase<TResult> query) where TResult : class
        {
            await BackUpMessage(query);
            return await Mediator.Send(query);
        }

        private void BasicSetup(IMediator mediator, IMediatRBackupService mediatRBackupService, IMediatRBackupLiteDbService mediatRBackupLiteDbService)
        {
            Mediator = mediator ?? throw new ArgumentNullException("Please register the meditaror object in your DI container");
            MediatRBackupService = mediatRBackupService;
            MediatRBackupLiteDbService = mediatRBackupLiteDbService;
        }

        private async Task BackUpMessage<TResult>(ActionBase<TResult> command, MediatRActionsEnum queueAction = MediatRActionsEnum.Sent, bool increaseRetryCounter = false, string additionalData = null, string errorData = null) where TResult : class
        {
            MediatRBackupLiteDbService?.CreateMessageBackup(command, queueAction, increaseRetryCounter, additionalData, errorData);

            if (MediatRBackupService != null && MediatRBackupService.UseExternalDatabase)
            {
                await MediatRBackupService.CreateMessageBackup(command, queueAction, increaseRetryCounter, additionalData, errorData).ConfigureAwait(false);
            }
        }
    }
}
