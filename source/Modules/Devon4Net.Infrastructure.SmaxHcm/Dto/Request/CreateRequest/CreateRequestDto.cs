using System.Collections.Generic;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Request.CreateRequest
{
    public class CreateRequestDto
    {
        public List<CreateRequestEntity> entities { get; set; }
        public string operation { get; set; }
    }
}
