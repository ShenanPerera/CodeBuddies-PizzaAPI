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
        private readonly ICustomerService _customerService;

        public OrderController(IOrderServices orderServices , ICustomerService customerService) 
        {
            _orderServices = orderServices;
            _customerService = customerService;
        }

        //POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(CreateOrder order , int customerID)
        {
            try
            {
                var newOrder = await _orderServices.CreateOrderAsync(order , customerID);
                return CreatedAtAction("GetOrder", new { id = newOrder.Id }, newOrder);
            }
            catch (DbUpdateConcurrencyException e) 
            { 
                return BadRequest(e.Message);
            }
        }

        //GET : api/Orders/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _orderServices.GetOrderByIdAsync(id);

            if (order.Customer == null)
            {
                return NotFound();
            }

            if(order == null)
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

            if(!orders.Any())
            {
                return NotFound("No orders found in the database");
            }

            return Ok(orders);
        }
    }
}
