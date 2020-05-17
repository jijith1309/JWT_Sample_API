using System;
using JWT_SampleApp.Controllers;
using JWT_SampleApp.DtoModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JWT_SampleApp.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void LoginTest()
        {
            LoginModel model = new LoginModel();
            model.LoginName = "test";
            model.Password = "a";
            AccountController accountController = new AccountController();
            var result = accountController.Login(model);
            Assert.IsNotNull(result);

        }
    }
}
