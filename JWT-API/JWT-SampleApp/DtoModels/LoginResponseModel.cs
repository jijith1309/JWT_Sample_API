using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JWT_SampleApp.DtoModels
{
    public class LoginResponseModel
    {
        public string Token { get; set; }
        public UserInfo User { get; set; }
    }
    public class UserInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}