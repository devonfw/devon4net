namespace Devon4Net.Application.WebAPI.Implementation.Business.AuthManagement.Dto
{
    /// <summary>
    /// CurrentUserResponse definition
    /// </summary>
    public class CurrentUserResponse
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// LastName
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// List of claims
        /// </summary>
        public List<CorporateBasicInfo> CorporateInfo { get; set; } 
    }
}