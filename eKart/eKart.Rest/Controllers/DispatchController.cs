using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using eKart.Messages;

namespace eKart.Rest.Controllers
{
    public class DispatchController : ApiController
    {
        private readonly IEndpointInstance endPoint;
        public DispatchController(IEndpointInstance endpoint)
        {
            this.endPoint = endpoint;
        }

        [Route("api/dispatch/order")]
        [HttpPost]
        public async Task<IHttpActionResult> PostMessage(eKart.Rest.Models.Order order) {


            await endPoint.Send("eKart.Order",new ProcessOrderCommand() {

                Address = order.Address,
                CusitomerId = order.CusitomerId,
                EmailId = order.EmailId,
                Price = order.Price

            }).ConfigureAwait(false);

            return Ok("Order dispatched successfully ");
        }
    }
}
