namespace Devon4Net.Infrastructure.CyberArk.Common.Const
{
    public static class CyberArkEndpointConst
    {
        public const string Logon = "/PasswordVault/API/auth/Cyberark/Logon";
        public const string Safes = "/PasswordVault/WebServices/PIMServices.svc/Safes";
        public const string UpdateSafes = "/PasswordVault/WebServices/PIMServices.svc/Safes/{0}/Members/{1}";
        public const string Accounts = "/PasswordVault/api/Accounts";
        public const string AccountsSearch = "search={0}";
        public const string AccountsFilter = "filter={0}";
        public const string AccountsSafeNameFilter = "filter=safeName eq {0}";
        public const string AccountRetrieveSuffix = "/Password/Retrieve";
        public const string Users = "/PasswordVault/WebServices/PIMServices.svc/Users";
        public const string GetUsers = "/PasswordVault/api/Users";
        public const string ResetPassword = "/PasswordVault/api/Users/ResetPassword";
        public const string GetUserGroups = "/PasswordVault/api/UserGroups";
        public const string AddUserToGroup = "/PasswordVault/WebServices/PIMServices.svc/Groups/{0}/Users";
    }
}
