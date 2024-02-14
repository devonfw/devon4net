using Devon4Net.Infrastructure.MediatR.Query;
using Devon4Net.Infrastructure.MediatR.Samples.Model;

namespace Devon4Net.Infrastructure.MediatR.Samples.Query
{
    public record GetUserQuery : QueryBase<UserDto>
    {
        public Guid UserId { get; set; }

        public GetUserQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}