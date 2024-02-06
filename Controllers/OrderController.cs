using CodeBuddies_PizzaAPI.DTOs;
using CodeBuddies_PizzaAPI.Models;
using CodeBuddies_PizzaAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeBuddies_PizzaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        //POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(CreateOrder order)
        {
            try
            {
                var newOrder = await _orderServices.CreateOrderAsync(order);
                return CreatedAtAction("GetOrder", new { id = newOrder.Id }, newOrder);
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            }
        }

        //GET : api/Orders/{id}
        [HttpGet("order/{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _orderServices.GetOrderByIdAsync(id);

            if (order.Customer == null)
            {
                return NotFound();
            }

            if (order == null)
            {
                return BadRequest("Order not found");
            }
            return order;
        }

        //GET : api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _orderServices.GetAllOrdersAsync();

            if (!orders.Any())
            {
                return NotFound("No orders found in the database");
            }

            return Ok(orders);
        }

        //Put: api/Orders/OrderDetailList/{id}
        [HttpPut("orderDetailList/{id}")]
        public async Task<IActionResult> PutOrderDetailList(int id)
        {
            var updateOrderDetails = await _orderServices.AddOrderDetailListtoOrders(id);
            return Ok(updateOrderDetails);
        }

        //Put: api/Orders/updateOnPreparation/{id}
        [HttpPut("onPreparation/{id}")]
        public async Task<ActionResult> PutOnPreparation(int id)
        {
            var updateOnPreparation = await _orderServices.UpdateOnPreparation(id);
            return Ok(updateOnPreparation);
        }


        //Put: api/Orders/updateOrderFulFillment/{id}
        [HttpPut("orderFullfilled/{id}")]
        public async Task<ActionResult> PutOrderFulFilled(int id)
        {
            var updateOrderFullFilled = await _orderServices.UpdateOrderFulFilled(id);
            return Ok(updateOrderFullFilled);
        }

        //Delete : api/Orders/deleteOrder/{id}
        [HttpDelete("deleteOrder/{id}")] 
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderServices.DeleteOrder(id);

            if(!result)
            {
                return NotFound("Order not found");
            }

            return Ok("Order deleted successfully");
        }
    }
}
