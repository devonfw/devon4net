using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OASP4Net.Business.Common.EmailManagement.Dto;
using OASP4Net.Business.Common.EmailManagement.Service;

namespace OASP4Net.Business.Common.EmailManagement.Controller
{
    /// <summary>
    /// API Methods for sending emails
    /// </summary>
    public class EmailController : Microsoft.AspNetCore.Mvc.Controller
    {
        /// <summary>
        /// 
        /// </summary>
        public EmailController()
        {
        }

        /// <summary>
        /// Sends the email
        /// </summary>
        /// <param name="emailDto"></param>
        /// <returns>True if the email has been sent</returns>
        /// <response code="201">Account created</response>
        /// <response code="400">Username already in use</response>
        [HttpOptions]
        [HttpPost]
        [AllowAnonymous]
        [Route("/mythaistar/services/rest/EmailManagement/v1/Email/Send")]
        public async Task<IActionResult> Send([FromBody] EmailDto emailDto)
        {
            bool result;
            try
            {
                IEmailService emailService = new EmailService();
                result = await emailService.Send(emailDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"{ex.Message} : {ex.InnerException}");

            }
            var serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.None
            };
            var json = JsonConvert.SerializeObject(result, serializerSettings);
            return new OkObjectResult(json);
        }
    }
}
