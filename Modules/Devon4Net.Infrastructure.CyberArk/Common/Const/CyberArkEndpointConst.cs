namespace Devon4Net.Infrastructure.CyberArk.Common.Const
{
    public static class CyberArkEndpointConst
    {
        public const string Logon = "/PasswordVault/API/auth/Cyberark/Logon";
        public const string Safes = "/PasswordVault/WebServices/PIMServices.svc/Safes";
        public const string Accounts = "/PasswordVault/api/Accounts";
        public const string AccountRetrieveSuffix = "/Password/Retrieve";
        public const string Users= "/PasswordVault/WebServices/PIMServices.svc/Users";
    }
}
