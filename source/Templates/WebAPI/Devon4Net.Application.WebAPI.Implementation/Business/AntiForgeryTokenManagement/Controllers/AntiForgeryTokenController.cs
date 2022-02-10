using Devon4Net.Infrastructure.Logger.Logging;
using Devon4Net.Infrastructure.Middleware.Middleware.Headers;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.Application.WebAPI.Implementation.Business.AntiForgeryTokenManagement.Controllers
{
    /// <summary>
    /// TODOs controller
    /// </summary>
    [EnableCors("CorsPolicy")]
    [ApiController]
    [Route("[controller]")]
    public class AntiForgeryTokenController : ControllerBase
    {
        private readonly IAntiforgery _antiForgeryToken;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="antiForgeryToken"></param>
        public AntiForgeryTokenController(IAntiforgery antiForgeryToken)
        {
            _antiForgeryToken = antiForgeryToken;
        }

        /// <summary>
        /// Gets the antiforgery token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/v1/antiforgeryToken/token")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Token()
        {
            Devon4NetLogger.Debug("Executing Token from controller AntiForgeryTokenController");
            var token = _antiForgeryToken.GetAndStoreTokens(HttpContext);
            HttpContext.Response.Cookies.Append(CustomMiddlewareHeaderTypeConst.XsrfToken, token.RequestToken);
            return Ok($"Please add the header {CustomMiddlewareHeaderTypeConst.XsrfToken}:{token.RequestToken}");
        }

        /// <summary>
        /// Gets the secured string result
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/v1/antiforgeryToken/hellosecured")]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult HelloSecured()
        {
            Devon4NetLogger.Debug("Executing HelloSecured from controller AntiForgeryTokenController");
            return Ok("You have reached a secured an AntiForgeryToken method!");
        }
    }
}
