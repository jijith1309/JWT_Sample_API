using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using JWT_SampleApp.Controllers;
using JWT_SampleApp.DtoModels;
using JWT_SampleApp.DtoModels.Order;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JWT_SampleApp.Tests.Controllers
{
    [TestClass]
    public class OrderControllerTest
    {
        [TestMethod]
        public void ViewMyOrders()
        {  // Arrange
            OrderController controller = new OrderController();
            // Act
            IHttpActionResult result = controller.ViewMyOrders(5);
            // Assert
            var contentResult = result as OkNegotiatedContentResult<ResponseModel<List<OrderModel>>>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content.Data);
        }
        [TestMethod]
        public void CancelOrder()
        {  // Arrange
            OrderController controller = new OrderController();
            // Act
            IHttpActionResult result = controller.CancelOrder(2);
            // Assert
            var contentResult = result as OkNegotiatedContentResult<ResponseModel<bool>>;
            Assert.IsNotNull(contentResult);
            Assert.IsTrue(contentResult.Content.Data);
        }
        [TestMethod]
        public void CreateOrderFromCart()
        {  // Arrange
            OrderController controller = new OrderController();
            OrderModel order = new OrderModel();
            order.CartId = 1;
            order.Address1 = "a1";
            order.Address2 = "Skyline";
            order.Country = "India";
            order.PhoneNumber = "43423423";
            order.PostalCode = "111111";
            order.UserId = 5;
            order.OrderDetailsList = new List<OrderDetailsModel>
            {
                new OrderDetailsModel{ProductId=1,Quantity=2}
            };
            // Act
            IHttpActionResult result = controller.CreateOrder(order);
            // Assert
            var contentResult = result as OkNegotiatedContentResult<ResponseModel<bool>>;
            Assert.IsNotNull(contentResult);
            Assert.IsTrue(contentResult.Content.Data);
        }
        [TestMethod]
        public void CreateDirectOrder()
        {  // Arrange
            OrderController controller = new OrderController();
            OrderModel order = new OrderModel();
           
            order.Address1 = "a1";
            order.Address2 = "Skyline";
            order.Country = "India";
            order.PhoneNumber = "43423423";
            order.PostalCode = "111111";
            order.UserId = 5;
            order.OrderDetailsList = new List<OrderDetailsModel>
            {
                new OrderDetailsModel{ProductId=1,Quantity=2}
            };
            // Act
            IHttpActionResult result = controller.CreateOrder(order);
            // Assert
            var contentResult = result as OkNegotiatedContentResult<ResponseModel<bool>>;
            Assert.IsNotNull(contentResult);
            Assert.IsTrue(contentResult.Content.Data);
        }

    }
}
