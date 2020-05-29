using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using JWT_SampleApp.Controllers;
using JWT_SampleApp.DtoModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JWT_SampleApp.Tests.Controllers
{
    [TestClass]
    public class ProductControllerTest
    {
        [TestMethod]
        public void GetAllProducts()
        {
            // Arrange
            ProductController controller = new ProductController();
            // Act
            IHttpActionResult result = controller.GetAllProducts();
            // Assert
            var contentResult = result as OkNegotiatedContentResult<DtoModels.ResponseModel<List<ProductModel>>>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content.Data);
            
        }
    }
}
