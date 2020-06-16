using System.Collections.Generic;

namespace Devon4Net.Infrastructure.CyberArk.Dto.Group
{
    public class GetGroupsResponseDto
    {
        public List<GroupDetail> value { get; set; }
        public int? count { get; set; }
    }
}
