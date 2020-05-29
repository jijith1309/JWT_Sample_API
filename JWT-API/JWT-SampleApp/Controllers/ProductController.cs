using JWT_SampleApp.DtoModels;
using JWT_SampleApp.filters;
using JWT_SampleApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JWT_SampleApp.Controllers
{
    [System.Web.Http.RoutePrefix("api/v1/Product")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [TokenAuthorise]
    public class ProductController : ApiController
    {
        [HttpGet]
        [Route("GetAllProducts")]
        public IHttpActionResult GetAllProducts()
        {
            try
            {

                ProductService service = new ProductService();

                var data = service.GetProductList();
                if (data != null && data.Count > 0)
                {
                    ResponseModel<List<ProductModel>> response = new ResponseModel<List<ProductModel>>();
                    response.Data = data;
                    response.Message = "Successfully retrived products";
                    return Ok(response);
                }
                return BadRequest("No products found");
            }
            catch (Exception ex)
            {
                return BadRequest("Error in GetAllProducts Api:" + ex.Message);
            }
        }
    }
}
