using JWT_SampleApp.DtoModels;
using JWT_SampleApp.filters;
using JWT_SampleApp.Services;
using JWT_SampleApp.TokenManagement;
using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JWT_SampleApp.Controllers
{
    [System.Web.Http.RoutePrefix("api/v1/ShoppingCart")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [TokenAuthorise]
    public class ShoppingCartController : ApiController
    {
        #region ViewMyCart

        [HttpGet]
        [Route("ViewMyCart")]
        public IHttpActionResult ViewMyShoppingCart(int userId)
        {
            try
            {
                ShoppingCartService service = new ShoppingCartService();
               
                var data = service.GetMyShoppingCartItems(userId);
                if (data != null && data.CartItemsList.Count>0)
                {
                    ResponseModel<ShoppingCartModel> response = new ResponseModel<ShoppingCartModel>();
                    response.Data = data;
                    response.Message = "Successfully retrived cart";
                    return Ok(response);
                }
                return BadRequest("No cart items found");
            }
            catch (Exception ex)
            {
                return BadRequest("Error in ViewMyShoppingCart Api:" + ex.Message);
            }
        }

        #endregion ViewMyCart

        #region Add to cart

        [HttpPost]
        [Route("AddToCart")]
        [ValidateModel]
        public IHttpActionResult AddtToCart(CartRequest request)
        {
            try
            {
                //string userId = this.User.GetClaimValue("UserId");
                //int user = Convert.ToInt32(userId);
                ShoppingCartService service = new ShoppingCartService();
                var data = service.AddToCart(request.UserId, request.ProductId, request.Quantity);
                if (data)
                {
                    ResponseModel<bool> response = new ResponseModel<bool>();
                    response.Data = data;
                    response.Message = "Item added to cart succcessfully";
                    return Ok(response);
                }
                return BadRequest("Item Could not be added.Please try again");
            }
            catch(Exception ex)
            {
                return BadRequest("Error in AddtToCart Api:" + ex.Message);
            }
           
        }
        #endregion

        #region Edit cart
        [HttpPut]
        [Route("EditCart")]
        public IHttpActionResult EditCart(CartRequest request)
        {
            try
            {
                //string userId = this.User.GetClaimValue("UserId");
                ShoppingCartService service = new ShoppingCartService();
                var data = service.AddToCart(request.UserId, request.ProductId, request.Quantity);
                if (data)
                {
                    ResponseModel<bool> response = new ResponseModel<bool>();
                    response.Data = data;
                    response.Message = "Item added to cart succcessfully";
                    return Ok(response);
                }
                return BadRequest("Item Could not be added.Please try again");
            }
            catch (Exception ex)
            {
                return BadRequest("Error in EditCart Api:" + ex.Message);
            }
        }

        #endregion

        #region Delete cart
        [HttpGet]
        [Route("DeleteCartItem/{cartItemId}")]
        public IHttpActionResult DeleteItem(int cartItemId,int userId)
        {
            try
            {
                if (cartItemId == 0)
                {
                    return BadRequest("Cart details needed");
                }
                ShoppingCartService service = new ShoppingCartService();
                var data = service.DeleteCartItem(userId, cartItemId);
                if (data)
                {
                    ResponseModel<bool> response = new ResponseModel<bool>();
                    response.Data = data;
                    response.Message = "Item removed from cart succcessfully";
                    return Ok(response);
                }
                return BadRequest("Item Could not be removed..Please try again");
            }
            catch(Exception ex)
            {
                return BadRequest("Error in DeleteItem Api:" + ex.Message);
            }
           
        }

        #endregion

    }
}