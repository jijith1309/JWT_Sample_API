using JWT_SampleApp.DtoModels;
using JWT_SampleApp.filters;
using JWT_SampleApp.Models;
using JWT_SampleApp.Services;
using JWT_SampleApp.TokenManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JWT_SampleApp.Controllers
{
    [System.Web.Http.RoutePrefix("api/v1/Account")]
    public class AccountController : ApiController
    {

        public AccountController()
        {

        }
        #region login
        [HttpPost]
        [Route("SignIn")]
        public IHttpActionResult Login(LoginModel model)
        {
            //ApplicationUser user = new ApplicationUser { Username = model.LoginName, Password = model.Passwoord };
            try
            {
                if(string.IsNullOrEmpty(model.LoginName) || string.IsNullOrEmpty(model.Password))
                {
                    return BadRequest("UserName and password are required");
                }
                UserAuthenticationService service = new UserAuthenticationService();
                var data = service.Login(model.LoginName, model.Password);
                if (data != null)
                {
                    return Ok(data);
                }
                return BadRequest("Error while login");
            }
            catch(Exception ex)
            {
                return BadRequest("Invalid Login:"+ex.Message);
            }
           
        }
        #endregion


        #region ValidateToken
        [TokenAuthorise]
        [HttpGet]
        [Route("ValidateToken")]
        public IHttpActionResult ValidateToken()
        {
          
            try
            {
               string name= this.User.GetClaimValue("FirstName");
                return Ok("Decrypted FirstName is:" + name);
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid token");
            }

        }
        #endregion
    }
}
