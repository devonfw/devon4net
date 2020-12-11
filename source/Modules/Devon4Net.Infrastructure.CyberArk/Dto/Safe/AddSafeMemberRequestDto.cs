using System.Collections.Generic;

namespace Devon4Net.Infrastructure.CyberArk.Dto.Safe
{
    public class AddSafeMemberRequestDto
    {
        public Member member { get; set; }
    }

    /// <summary>
    /// MembershipExpirationDate has the format : &lt;MM\DD\YY or empty for no expiration&gt;
    /// </summary>
    public class Member
    {
        public string MemberName { get; set; }
        public string SearchIn { get; set; }
        public string MembershipExpirationDate { get; set; }
        public List<Permission> Permissions { get; set; }
    }

    /// <summary>
    /// Please check MemberPermissionsConst
    /// </summary>
    public class Permission
    {
        public string Key { get; set; }
        public object Value { get; set; }
    }
}
