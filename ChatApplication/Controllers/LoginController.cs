using ChatApplicationService.Infc;
using ChatApplicationModel.Dto;
using ChatApplicationModel.Model;
using ChatApplicationModel.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace ChatApplicationModel.Controllers
{
    [Route("/user")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public LoginController(ILoginService service)
        {
            this.LoginService = service;
        }
        public ILoginService LoginService { get; set; }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var UserData = LoginService.Login(loginDto);
                if (UserData != null)
                {
                    var token = LoginService.GenerateJSONWebToken(UserData.id);
                    return this.Ok(new ResponseEntity() { httpStatusCode = HttpStatusCode.OK, 
                        message = "Login Successfully", 
                        data = UserData, 
                        token = token });
                }
            }
            catch
            {
                return this.BadRequest(new ResponseEntity()
                    {
                        httpStatusCode = HttpStatusCode.BadRequest,
                        message = "Bad Request",
                        data = null,
                        token = ""
                    });
            }
            return this.Ok(new ResponseEntity() {
                httpStatusCode = HttpStatusCode.NoContent, 
                message = "Login Failed", 
                data = null, 
                token = ""
            });
        }

        [HttpPost]
        [Route("registration")]
        public IActionResult Register([FromBody] User user)
        {
            string UserData;
            try
            {
                UserData = LoginService.Registration(user);
                if (UserData != "" && UserData != null)
                {
                    return this.Ok(new ResponseEntity()
                    {
                        httpStatusCode = HttpStatusCode.OK,
                        message = UserData,
                        data = "",
                        token = ""
                    });
                }
            }
            catch
            {
                return this.BadRequest(
                    new ResponseEntity()
                    {
                        httpStatusCode = HttpStatusCode.BadRequest,
                        message = "Bad Request",
                        data = null,
                        token = ""
                    });
            }
            return this.Ok(new ResponseEntity() { 
                httpStatusCode = HttpStatusCode.Found,
                message = UserData,
                data = "",
                token = ""
            });
        }

        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("hello")]
        public string Hello()
        {
            return "Hello";
        }

    }
}
