using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthWind.UseCases.CreateOrder;

namespace NorthWind.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator Mediator;
        public OrderController(IMediator mediator)=>Mediator = mediator;

        [HttpPost("create-order")]
        public async Task<ActionResult<int>> CreateOrder(CreateOrderInputPort orderparams)
        {
            return await Mediator.Send(orderparams);
        }
    }
}
