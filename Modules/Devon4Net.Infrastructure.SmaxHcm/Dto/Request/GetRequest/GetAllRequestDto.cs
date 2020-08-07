using System;
using System.Collections.Generic;
using System.Text;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Request.GetRequest
{

    public class GetAllRequestDto
    {
        public List<EntityRequest> entities { get; set; }
        public RequestMeta meta { get; set; }
    }
}
