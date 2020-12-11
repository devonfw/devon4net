using System.Collections.Generic;

namespace Devon4Net.Infrastructure.CyberArk.Dto.Account
{

    public class GetAccountsResponseDto
    {
        public List<AccountDetail> value { get; set; }
        public int count { get; set; }
    }
}