using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AnsibleTower.Dto.Common
{
    public class Tokens
    {
        public int count { get; set; }
        public IList<object> results { get; set; }
    }
}