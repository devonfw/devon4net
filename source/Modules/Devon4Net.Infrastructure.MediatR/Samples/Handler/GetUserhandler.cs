using Devon4Net.Infrastructure.MediatR.Domain.ServiceInterfaces;
using Devon4Net.Infrastructure.MediatR.Handler;
using Devon4Net.Infrastructure.MediatR.Samples.Model;
using Devon4Net.Infrastructure.MediatR.Samples.Query;

namespace Devon4Net.Infrastructure.MediatR.Samples.Handler
{
    public class GetUserhandler: MediatrRequestHandler<GetUserQuery, UserDto>
    {
        public GetUserhandler(IMediatRBackupService mediatRBackupService, IMediatRBackupLiteDbService mediatRBackupLiteDbService) : base(mediatRBackupService, mediatRBackupLiteDbService)
        {
        }

        public GetUserhandler(IMediatRBackupLiteDbService mediatRBackupLiteDbService) : base(mediatRBackupLiteDbService)
        {
        }

        public GetUserhandler(IMediatRBackupService mediatRBackupService) : base(mediatRBackupService)
        {
        }

        /// <summary>
        /// Perform yopur queries against repositories, API...
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<UserDto> HandleAction(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = new UserDto
            {
                Id = request.UserId,
                Name = "Retrived Name!",
                SurName = "Retrieved Surname!"
            };

            return Task.FromResult(user);
        }
    }
}
