using System.Collections.Generic;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Request.AbandonRequest
{
    public class AbandonRequestDto
    {
        public List<AbandonRequestEntity> entities { get; set; }
        public string operation { get; set; }
    }
}
