using JWT_SampleApp.DtoModels;
using JWT_SampleApp.filters;
using JWT_SampleApp.Services;
using JWT_SampleApp.TokenManagement;
using System;
using System.Dynamic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JWT_SampleApp.Controllers
{

    [System.Web.Http.RoutePrefix("api/v1/Account")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountController : ApiController
    {
        private readonly dynamic response;

        public AccountController()
        {
            response = new ExpandoObject();
        }

        #region login

        [ValidateModel]
        [HttpPost]
        [Route("SignIn")]
        public IHttpActionResult Login(LoginModel model)
        {
            try
            {
                UserAuthenticationService service = new UserAuthenticationService();
                var data = service.Login(model.LoginName, model.Password);
                if (data != null)
                {
                    return Ok(data);
                }
                return BadRequest("Error while login");
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid Login:" + ex.Message);
            }
        }

        #endregion login

        #region Register

        [ValidateModel]
        [HttpPost]
        [Route("Register")]
        public IHttpActionResult Register(RegisterModel model)
        {
            try
            {
                UserAuthenticationService service = new UserAuthenticationService();
                model.Password = RC4.Encrypt("password", model.Password);
                var data = service.Register(model);
                if (data)
                {
                    response.IsError = false;
                    response.Message = "Successfully created new user";
                    return Ok(data);
                }
                return BadRequest("Error while login");
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid Login:" + ex.Message);
            }
        }

        #endregion Register

        #region Edit User
        [TokenAuthorise]
        [ValidateModel]
        [HttpPost]
        [Route("Edit")]
        public IHttpActionResult Edituser(RegisterModel model,int userId)
        {
            try
            {
                UserAuthenticationService service = new UserAuthenticationService();
                model.Password = RC4.Encrypt("password", model.Password);
                var data = service.EditUser(model, userId);
                if (data)
                {
                    response.IsError = false;
                    response.Message = "Successfully updated user";
                    return Ok(data);
                }
                return BadRequest("Error while updating user");
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid edit operation:" + ex.Message);
            }
        }

        #endregion Register

        #region View User
        [TokenAuthorise]
        [HttpGet]
        [Route("UserDetails")]
        public IHttpActionResult ViewUser()
        {
            try
            {
                UserAuthenticationService service = new UserAuthenticationService();
               
                int newUserId = Convert.ToInt32(this.User.GetClaimValue("UserId"));
                var data = service.ViewUser(newUserId);
                if (data != null)
                {
                    response.IsError = false;
                    response.Message = "Successfully retrieved user details";
                    return Ok(data);
                }
                return BadRequest("Unable to find user" );
            }
            catch (Exception ex)
            {
                return BadRequest("Error :" + ex.Message);
            }
        }

        #endregion View User

        #region ValidateToken

        [TokenAuthorise]
        [HttpGet]
        [Route("ValidateToken")]
        public IHttpActionResult ValidateToken()
        {
            try
            {
                string name = this.User.GetClaimValue("FirstName");
                return Ok("Decrypted FirstName is:" + name);
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid token");
            }
        }

        #endregion ValidateToken
    }
}