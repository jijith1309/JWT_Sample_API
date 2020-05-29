using JWT_SampleApp.DtoModels;
using JWT_SampleApp.DtoModels.Order;
using JWT_SampleApp.filters;
using JWT_SampleApp.Services;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JWT_SampleApp.Controllers
{
    [System.Web.Http.RoutePrefix("api/v1/Order")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [TokenAuthorise]
    public class OrderController : ApiController
    {
        #region ViewAllOrders

        [HttpGet]
        [Route("ViewMyOrders")]
        public IHttpActionResult ViewMyOrders(int userId)
        {
            try
            {
                if (userId == 0)
                {
                    return BadRequest("UserId is needed");
                }
                OrderService service = new OrderService();

                var data = service.ViewAllOrders(userId);
                if (data != null && data.Count > 0)
                {
                    ResponseModel<List<OrderModel>> response = new ResponseModel<List<OrderModel>>();
                    response.Data = data;
                    response.Message = "Successfully retrived orders";
                    return Ok(response);
                }
                return BadRequest("No orders found");
            }
            catch (Exception ex)
            {
                return BadRequest("Error in ViewMyOrders Api:" + ex.Message);
            }
        }

        #endregion ViewAllOrders

        #region CancelOrder

        [HttpGet]
        [Route("CancelOrder")]
        public IHttpActionResult CancelOrder(int orderId)
        {
            try
            {
                OrderService service = new OrderService();

                if (orderId == 0)
                {
                    return BadRequest("Orderid is needed");
                }
                var data = service.CancelOrder(orderId);
                if (data )
                {
                    ResponseModel<bool> response = new ResponseModel<bool>();
                    response.Data = data;
                    response.Message = "Successfully cancelled order";
                    return Ok(response);
                }
                return BadRequest("Falied to cancel order");
            }
            catch (Exception ex)
            {
                return BadRequest("Error in CancelOrder Api:" + ex.Message);
            }
        }

        #endregion CancelOrder

        #region CreateOrder

        [HttpPost]
        [Route("CreateOrder")]
        [ValidateModel]
        public IHttpActionResult CreateOrder(OrderModel order)
        {
            try
            {
                OrderService service = new OrderService();

                var data = service.CreateOrder(order);
                if (data)
                {
                    ResponseModel<bool> response = new ResponseModel<bool>();
                    response.Data = data;
                    response.Message = "Successfully created order";
                    return Ok(response);
                }
                return BadRequest("Falied to create order");
            }
            catch (Exception ex)
            {
                return BadRequest("Error in create order Api:" + ex.Message);
            }
        }

        #endregion CreateOrder
    }
}