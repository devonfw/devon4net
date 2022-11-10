using Devon4Net.Infrastructure.Common;
using Devon4Net.Infrastructure.MediatR.Common;
using Devon4Net.Infrastructure.MediatR.Domain.ServiceInterfaces;
using MediatR;

namespace Devon4Net.Infrastructure.MediatR.Handler
{
    public abstract class MediatrRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private IMediatRBackupService MediatRBackupService { get; set; }
        private IMediatRBackupLiteDbService MediatRBackupLiteDbService { get; set; }

        protected MediatrRequestHandler(IMediatRBackupService mediatRBackupService, IMediatRBackupLiteDbService mediatRBackupLiteDbService)
        {
            BasicSetup(mediatRBackupService, mediatRBackupLiteDbService);
        }

        protected MediatrRequestHandler(IMediatRBackupService mediatRBackupService)
        {
            BasicSetup(mediatRBackupService, null);
        }

        protected MediatrRequestHandler(IMediatRBackupLiteDbService mediatRBackupLiteDbService)
        {
            BasicSetup(null, mediatRBackupLiteDbService);
        }

        protected MediatrRequestHandler()
        {
            BasicSetup(null, null);
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            MediatrActions status;
            TResponse result = default;
            try
            {
                result = await HandleAction(request, cancellationToken).ConfigureAwait(false);
                status = MediatrActions.Handled;
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
                status = MediatrActions.Error;
            }
            await BackUpMessage(request, status).ConfigureAwait(false);
            return result;
        }

        public abstract Task<TResponse> HandleAction(TRequest request, CancellationToken cancellationToken);

        private void BasicSetup(IMediatRBackupService mediatRBackupService, IMediatRBackupLiteDbService mediatRBackupLiteDbService)
        {
            MediatRBackupService = mediatRBackupService;
            MediatRBackupLiteDbService = mediatRBackupLiteDbService;
        }

        private async Task BackUpMessage(TRequest request, MediatrActions queueAction = MediatrActions.Sent, bool increaseRetryCounter = false, string additionalData = null, string errorData = null)
        {
            MediatRBackupLiteDbService?.CreateResponseMessageBackup(request, queueAction, increaseRetryCounter, additionalData, errorData);

            if (MediatRBackupService?.UseExternalDatabase == true)
            {
                await MediatRBackupService.CreateResponseMessageBackup(request, queueAction, increaseRetryCounter, additionalData, errorData).ConfigureAwait(false);
            }
        }
    }
}
