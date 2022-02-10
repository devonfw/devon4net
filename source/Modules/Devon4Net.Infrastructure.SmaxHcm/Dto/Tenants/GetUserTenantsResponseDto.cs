using System.Collections.Generic;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Tenants
{

    public class GetUserTenantsResponseDto
    {
        public List<TenantEntityDto> entities { get; set; }
        public int totalCount { get; set; }
    }
}
