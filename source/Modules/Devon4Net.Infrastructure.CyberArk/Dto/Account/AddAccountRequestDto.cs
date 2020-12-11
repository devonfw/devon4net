namespace Devon4Net.Infrastructure.CyberArk.Dto.Account
{
    public class AddAccountRequestDto
    {
        public string name { get; set; }
        public string address { get; set; }
        public string userName { get; set; }
        public string platformId { get; set; }
        public string safeName { get; set; }
        public string secretType { get; set; }
        public string secret { get; set; }
        public Secretmanagement secretManagement { get; set; }
        public Remotemachinesaccess remoteMachinesAccess { get; set; }
    }
}
