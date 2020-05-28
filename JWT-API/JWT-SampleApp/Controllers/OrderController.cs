using JWT_SampleApp.filters;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JWT_SampleApp.Controllers
{
    [System.Web.Http.RoutePrefix("api/v1/Order")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [TokenAuthorise]
    public class OrderController : ApiController
    {
    }
}