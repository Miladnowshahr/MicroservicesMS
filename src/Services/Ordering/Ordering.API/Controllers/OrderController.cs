using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Commands.CheckoutOrder;
using Ordering.Application.Features.Commands.DeleteOrder;
using Ordering.Application.Features.Commands.UpdateOrder;
using Ordering.Application.Features.Queries.GetOrdersList;

namespace Ordering.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userName}", Name = "GetOrder")]
        [ProducesResponseType(typeof(IEnumerable<OrdersVm>), 200)]
        public async Task<ActionResult<IEnumerable<OrdersVm>>> GetOrdersByUserName(string userName)
        {
            var query = new GetOrdersListQuery(userName);
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }

        [HttpPost(Name = "CheckoutOrder")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<int>> CheckoutOrder([FromBody] CheckoutOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut(Name = "UpdateOrder")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteOrder")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateOrder([FromBody] DeleteOrderCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}