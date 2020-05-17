using JWT_SampleApp.DtoModels;
using JWT_SampleApp.Models;
using JWT_SampleApp.TokenManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JWT_SampleApp.Services
{
    public class UserAuthenticationService
    {
        public LoginResponseModel Login(string userName,string password)
        {
            LoginResponseModel responseModel = new LoginResponseModel();
            try
            {
                using (JWTSampleDbContext dbContext = new JWTSampleDbContext())
                {
                    //Check user exists in db
                    var appUser = dbContext.ApplicationUser.Where(a => a.Username == userName && a.Password == password).FirstOrDefault();
                    if (appUser != null)
                    {
                        //Generate token
                        responseModel.Token = TokenProvider.GenerateToken(appUser);

                        UserInfo userInfo = new UserInfo();
                        userInfo.UserId = appUser.UserId;
                        userInfo.UserName = appUser.Username;
                        userInfo.Email = appUser.Password;
                        responseModel.User = userInfo;
                    }

                }
                return responseModel;
            }
            catch(Exception ex)
            {
                throw new Exception("Invalid Login");
            }
        }
    }
}