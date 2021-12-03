using Devon4Net.Infrastructure.CyberArk.Dto.Account;
using Devon4Net.Infrastructure.CyberArk.Dto.Group;
using Devon4Net.Infrastructure.CyberArk.Dto.Safe;
using Devon4Net.Infrastructure.CyberArk.Dto.User;
using Devon4Net.Infrastructure.CyberArk.Handler;
using Devon4Net.Infrastructure.Log;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.Application.WebAPI.Implementation.Business.CyberArkManagement
{
    [ApiController]
    [Route("[controller]")]
    public class CyberArkController : ControllerBase
    {
        private ICyberArkHandler CyberArkHandler { get; }

        public CyberArkController(ICyberArkHandler cyberArkHandler)
        {
            CyberArkHandler = cyberArkHandler;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/cyberark/logon")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(string user, string password)
        {
            Devon4NetLogger.Debug("Executing Login from controller CyberArk");
            return Ok(await CyberArkHandler.Logon(user, password).ConfigureAwait(false));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/cyberark/safes")]
        [ProducesResponseType(typeof(GetSafesResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSafes(string authToken = null)
        {
            Devon4NetLogger.Debug("Executing Login from controller CyberArk");
            return Ok(await CyberArkHandler.GetSafes(authToken).ConfigureAwait(false));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/cyberark/safe")]
        [ProducesResponseType(typeof(GetSafeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSafe(string idSafe, string authToken = null)
        {
            Devon4NetLogger.Debug("Executing Login from controller CyberArk");
            return Ok(await CyberArkHandler.GetSafe(idSafe, authToken).ConfigureAwait(false));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/cyberark/safe")]
        [ProducesResponseType(typeof(AddSafeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddSafe(AddSafeRequestDto idSafe, string authToken = null)
        {
            Devon4NetLogger.Debug("Executing Login from controller CyberArk");
            return Ok(await CyberArkHandler.AddSafe(idSafe, authToken).ConfigureAwait(false));
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("/v1/cyberark/safe")]
        [ProducesResponseType(typeof(UpdateSafeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSafe(UpdateSafeRequestDto updateSafeRequest, string authToken = null)
        {
            Devon4NetLogger.Debug("Executing Login from controller CyberArk");
            return Ok(await CyberArkHandler.UpdateSafe(updateSafeRequest, authToken).ConfigureAwait(false));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/cyberark/accounts")]
        [ProducesResponseType(typeof(GetAccountsResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAccounts(string searchCriteria = null, string filterCriteria = null, bool useSafeFilter = true,  string authToken = null)
        {
            Devon4NetLogger.Debug("Executing Login from controller CyberArk");
            return Ok(await CyberArkHandler.GetAccounts(searchCriteria, filterCriteria, useSafeFilter, authToken).ConfigureAwait(false));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/cyberark/account")]
        [ProducesResponseType(typeof(AccountDetail), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAccount(string idAccount, string authToken = null)
        {
            Devon4NetLogger.Debug("Executing Login from controller CyberArk");
            return Ok(await CyberArkHandler.GetAccount(idAccount, authToken).ConfigureAwait(false));
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/cyberark/account")]
        [ProducesResponseType(typeof(AddAccountResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAccount(AddAccountRequestDto accountRequest, string authToken = null)
        {
            Devon4NetLogger.Debug("Executing Login from controller CyberArk");
            return Ok(await CyberArkHandler.AddAccount(accountRequest, authToken).ConfigureAwait(false));
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("/v1/cyberark/account")]
        [ProducesResponseType(typeof(AddAccountResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAccount(string idAccount, string authToken = null)
        {
            Devon4NetLogger.Debug("Executing Login from controller CyberArk");
            return Ok(await CyberArkHandler.DeleteAccount(idAccount, authToken).ConfigureAwait(false));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/cyberark/retrieveaccount")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RetrieveAccount(string idAccount, string authToken = null)
        {
            Devon4NetLogger.Debug("Executing Login from controller CyberArk");
            return Ok(await CyberArkHandler.RetrieveAccount(idAccount, authToken).ConfigureAwait(false));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/cyberark/users")]
        [ProducesResponseType(typeof(GetUserResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsers(GetUsersRequestDto usersRequest, string authToken = null)
        {
            Devon4NetLogger.Debug("Executing Login from controller CyberArk");
            return Ok(await CyberArkHandler.GetUsers(usersRequest, authToken).ConfigureAwait(false));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/cyberark/user")]
        [ProducesResponseType(typeof(GetUserResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUser(string userName, string authToken = null)
        {
            Devon4NetLogger.Debug("Executing Login from controller CyberArk");
            return Ok(await CyberArkHandler.GetUser(userName, authToken).ConfigureAwait(false));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/cyberark/user")]
        [ProducesResponseType(typeof(GetUserResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddUser(AddUserRequestDto userRequest, string authToken = null)
        {
            Devon4NetLogger.Debug("Executing Login from controller CyberArk");
            return Ok(await CyberArkHandler.AddUser(userRequest, authToken).ConfigureAwait(false));
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("/v1/cyberark/user")]
        [ProducesResponseType(typeof(GetUserResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUser(UpdateUserRequestDto userRequest, string userName, string authToken = null)
        {
            Devon4NetLogger.Debug("Executing Login from controller CyberArk");
            return Ok(await CyberArkHandler.UpdateUser(userRequest, userName, authToken).ConfigureAwait(false));
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("/v1/cyberark/user")]
        [ProducesResponseType(typeof(DeletedUser), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string userName, string authToken = null)
        {
            Devon4NetLogger.Debug("Executing Login from controller CyberArk");
            return Ok(await CyberArkHandler.DeleteUser(userName, authToken).ConfigureAwait(false));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/cyberark/resetuserpassword")]
        [ProducesResponseType(typeof(GetUserResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ResetUserPassword(string idUser, string newPassword, string authToken = null)
        {
            Devon4NetLogger.Debug("Executing Login from controller CyberArk");
            return Ok(await CyberArkHandler.ResetUserPassword(idUser, newPassword, authToken).ConfigureAwait(false));
        }


        [HttpPut]
        [AllowAnonymous]
        [Route("/v1/cyberark/updatesafemember")]
        [ProducesResponseType(typeof(UpdateSafeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSafeMember(string safeName, AddSafeMemberRequestDto updateSafeMember, string authToken = null)
        {
            Devon4NetLogger.Debug("Executing Login from controller CyberArk");
            return Ok(await CyberArkHandler.UpdateSafeMember(safeName,  updateSafeMember, authToken).ConfigureAwait(false));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/cyberark/addsafemember")]
        [ProducesResponseType(typeof(AddSafeMemberRequestDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddSafeMember(string safeName, AddSafeMemberRequestDto updateSafeMember, string authToken = null)
        {
            Devon4NetLogger.Debug("Executing Login from controller CyberArk");
            return Ok(await CyberArkHandler.AddSafeMember(safeName, updateSafeMember, authToken).ConfigureAwait(false));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/v1/cyberark/getusergroups")]
        [ProducesResponseType(typeof(GetGroupsResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserGroups( string authToken = null)
        {
            Devon4NetLogger.Debug("Executing Login from controller CyberArk");
            return Ok(await CyberArkHandler.GetUserGroups(authToken).ConfigureAwait(false));
        }


        /// <summary>
        /// This method is only available in version 11+
        /// </summary>
        /// <param name="createGroupRequest"></param>
        /// <param name="authToken"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/cyberark/createusergroup")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUserGroup(CreateGroupRequestDto createGroupRequest, string authToken = null)
        {
            Devon4NetLogger.Debug("Executing Login from controller CyberArk");
            return Ok(await CyberArkHandler.CreateUserGroup(createGroupRequest, authToken).ConfigureAwait(false));
        }

        /// <summary>
        /// /v1/cyberark/addusertogroup
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="groupName"></param>
        /// <param name="authToken"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/v1/cyberark/addusertogroup")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddUserToGroup(string userName, string groupName, string authToken = null)
        {
            Devon4NetLogger.Debug("Executing Login from controller CyberArk");
            return Ok(await CyberArkHandler.AddUserToGroup(userName, groupName, authToken).ConfigureAwait(false));
        }
        
    }
}
