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
            model.LoginName = "tesr";
            model.Password = "a";
            AccountController accountController = new AccountController();
            // Act
            IHttpActionResult result = accountController.Login(model);
            // Assert
            var contentResult = result as OkNegotiatedContentResult<ResponseModel<LoginResponseModel>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content.Data);
        }

        [TestMethod]
        public void RegisterTest()
        {
            // Arrange
            RegisterModel model = new RegisterModel();
            model.Username = "jijith1309";
            model.Password = "a";
            model.EmailId = "jijith1309@gmail.com";
            model.FirstName = "jijith";
            model.LastName="MS";
            model.PhoneNumber = "8547208190";
           
            AccountController accountController = new AccountController();
            // Act
            IHttpActionResult result = accountController.Register(model);
            // Assert
            var contentResult = result as OkNegotiatedContentResult<ResponseModel<bool>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsTrue(contentResult.Content.Data);

        }

        [TestMethod]
        public void EditUserTest()
        {
            // Arrange
            RegisterModel model = new RegisterModel();
            model.Username = "jijith1309";
            model.Password = "a";
            model.EmailId = "jijith1309@gmail.com";
            model.FirstName = "jijith1";
            model.LastName = "MS";
            model.PhoneNumber = "8547208190";
            int userId = 5;
            AccountController accountController = new AccountController();
            // Act
            IHttpActionResult result = accountController.Edituser(model);
            // Assert
            var contentResult = result as OkNegotiatedContentResult<ResponseModel<bool>>;
            Assert.IsNotNull(contentResult);
            Assert.IsTrue(contentResult.Content.Data);
        }

        [TestMethod]
        public void ViewUserDetails()
        {
            // Arrange
           
            AccountController accountController = new AccountController();
            // Act
            IHttpActionResult result = accountController.ViewUser(6);
            // Assert
            var contentResult = result as OkNegotiatedContentResult<ResponseModel<RegisterModel>>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content.Data);
        }
    }
}