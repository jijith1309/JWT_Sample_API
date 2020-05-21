using JWT_SampleApp.DtoModels;
using JWT_SampleApp.Models;
using JWT_SampleApp.TokenManagement;
using System;
using System.Linq;

namespace JWT_SampleApp.Services
{
    public class UserAuthenticationService
    {
        public LoginResponseModel Login(string userName, string password)
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
            catch (Exception ex)
            {
                throw new Exception("Invalid Login");
            }
        }

        public bool Register(RegisterModel model)
        {
            try
            {
                using (JWTSampleDbContext dbContext = new JWTSampleDbContext())
                {
                    ApplicationUser user = new ApplicationUser();
                    user.EmailId = model.EmailId;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Password = model.Password;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Username = model.Username;
                    user.CreatedOn = DateTime.Now;
                    //Check user exists in db
                    var appUser = dbContext.ApplicationUser.Where(a => a.Username == model.Username || a.EmailId == model.EmailId).FirstOrDefault();
                    if (appUser != null)
                    {
                        throw new Exception("This username or email id already exists");
                    }
                    //dbContext.Set<ApplicationUser>().Add(user);
                    dbContext.ApplicationUser.Add(user);
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool EditUser(RegisterModel model, int userId)
        {
            try
            {
                using (JWTSampleDbContext dbContext = new JWTSampleDbContext())
                {
                    var user = dbContext.ApplicationUser.Where(a => a.UserId == userId).FirstOrDefault();
                    if (user == null)
                    {
                        throw new Exception("This userId does not exist");
                    }
                    //ApplicationUser user = new ApplicationUser();
                    user.EmailId = model.EmailId;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Password = model.Password;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Username = model.Username;

                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in EditUser:" + ex.Message);
            }
        }

        public RegisterModel ViewUser(int userId)
        {
            try
            {
                RegisterModel model = null;
                using (JWTSampleDbContext dbContext = new JWTSampleDbContext())
                {
                    var user = dbContext.ApplicationUser.Where(a => a.UserId == userId).FirstOrDefault();
                    if (user != null)
                    {
                        model = new RegisterModel();
                        model.UserId = user.UserId;
                        model.Username = user.Username;
                        model.PhoneNumber = user.PhoneNumber;
                        model.FirstName = user.FirstName;
                        model.LastName = user.LastName;
                        model.EmailId = user.EmailId;
                        model.Password = RC4.Decrypt("password", user.Password);
                        model.CreatedOn = user.CreatedOn;
                    }

                }
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching user:" + ex.Message);
            }
        }
    }
}