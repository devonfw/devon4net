namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Users
{

    public class GetUsersResponseDto
    {
        public List<UserEntityDto> entities { get; set; }
        public int totalCount { get; set; }
    }
}
