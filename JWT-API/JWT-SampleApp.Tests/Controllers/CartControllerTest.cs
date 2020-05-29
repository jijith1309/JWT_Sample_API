using JWT_SampleApp.Controllers;
using JWT_SampleApp.DtoModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http;
using System.Web.Http.Results;

namespace JWT_SampleApp.Tests.Controllers
{
    [TestClass]
    public class CartControllerTest
    {
        [TestMethod]
        public void ViewMyCart()
        {  // Arrange
            ShoppingCartController controller = new ShoppingCartController();
            // Act
            IHttpActionResult result = controller.ViewMyShoppingCart(5);
            // Assert
            var contentResult = result as OkNegotiatedContentResult<DtoModels.ResponseModel<ShoppingCartModel>>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content.Data);
        }

        [TestMethod]
        public void AddToCart()
        {
            // Arrange
            CartRequest request = new CartRequest();
            request.ProductId = 2;
            request.Quantity = 2;
            request.UserId = 5;

            ShoppingCartController controller = new ShoppingCartController();
            // Act
            IHttpActionResult result = controller.AddtToCart(request);
            // Assert
            var contentResult = result as OkNegotiatedContentResult<ResponseModel<bool>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsTrue(contentResult.Content.Data);
        }
        [TestMethod]
        public void EditCart()
        {
            // Arrange
            CartRequest request = new CartRequest();
            request.ProductId = 1;
            request.Quantity = 1;
            request.UserId = 5;

            ShoppingCartController controller = new ShoppingCartController();
            // Act
            IHttpActionResult result = controller.EditCart(request);
            // Assert
            var contentResult = result as OkNegotiatedContentResult<ResponseModel<bool>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsTrue(contentResult.Content.Data);
        }

        [TestMethod]
        public void DeleteCartItem()
        {
            // Arrange

            int cartItemId = 3;
            int userId = 5;
            ShoppingCartController controller = new ShoppingCartController();
            // Act
            IHttpActionResult result = controller.DeleteItem(cartItemId, userId);
            // Assert
            var contentResult = result as OkNegotiatedContentResult<ResponseModel<bool>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsTrue(contentResult.Content.Data);
        }
    }
}