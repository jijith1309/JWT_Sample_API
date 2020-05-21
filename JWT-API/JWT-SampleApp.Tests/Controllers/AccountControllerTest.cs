using JWT_SampleApp.Controllers;
using JWT_SampleApp.DtoModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http;
using System.Web.Http.Results;

namespace JWT_SampleApp.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void LoginTest()
        {
            // Arrange
            LoginModel model = new LoginModel();
            model.LoginName = "test";
            model.Password = "a";
            AccountController accountController = new AccountController();
            // Act
            IHttpActionResult result = accountController.Login(model);
            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void RegisterTest()
        {
            // Arrange
            RegisterModel model = new RegisterModel();
            model.Username = "test";
            model.Password = "a";
            AccountController accountController = new AccountController();
            // Act
            IHttpActionResult result = accountController.Register(model);
            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void EditUserTest()
        {
            // Arrange
            RegisterModel model = new RegisterModel();
            model.Username = "test";
            model.Password = "a";
            int userId = 1;
            AccountController accountController = new AccountController();
            // Act
            IHttpActionResult result = accountController.Edituser(model, userId);
            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }
    }
}