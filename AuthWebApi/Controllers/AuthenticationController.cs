using AuthWebApi.Authentication.Interface;
using AuthWebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Authenticate")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJWTTokenManager _jwtTokenManager;

        public AuthenticationController(IJWTTokenManager jwtTokenManager)
        {
            _jwtTokenManager = jwtTokenManager;
        }

        [AllowAnonymous]
        [HttpPost("GetToken")]        
        public IActionResult GetToken([FromBody]UserCredential userCredential)
        {
            var token = _jwtTokenManager.GetToken(userCredential.UserName, userCredential.PassWord);
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            return Ok(token);
        }

    }
}