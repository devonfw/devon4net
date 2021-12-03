namespace Devon4Net.Infrastructure.CyberArk.Dto.Account
{
    public class AccountDetail
    {
        public string id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string userName { get; set; }
        public string platformId { get; set; }
        public string safeName { get; set; }
        public string secretType { get; set; }
        public Platformaccountproperties platformAccountProperties { get; set; }
        public Secretmanagement secretManagement { get; set; }
        public int? createdTime { get; set; }
    }
}