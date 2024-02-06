using CodeBuddies_PizzaAPI.Data;
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
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailServices _orderDetailServices;
        public OrderDetailController(IOrderDetailServices orderDetailServices)
        {
            _orderDetailServices = orderDetailServices;
        }

        //Post: api/OrderDetails
        [HttpPost]
        public async Task<ActionResult<OrderDetail>> PostOrderDetail(OrderDetail orderDetail, int orderID, int productID)
        {
            try
            {
                var newOrderDetail = await _orderDetailServices.AddOrderDetailAsync(orderDetail, orderID, productID);
                return CreatedAtAction("GetOrderDetail", new { id = newOrderDetail.Id }, newOrderDetail);
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetails()
        {
            var orders = await _orderDetailServices.GetAllOrderDetailsAsync();

            if (!orders.Any())
            {
                return NotFound("No orderDetailss found in the database");
            }

            return Ok(orders);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetail(int id)
        {
            var orderDetail = await _orderDetailServices.GetOrderDetailByIdAsync(id);

            if (orderDetail == null)
            {
                return BadRequest("OrderDetail not found");
            }
            return orderDetail;
        }

        //DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrderDetail(int id)
        {
            var result = await _orderDetailServices.DeleteOrderDetailAsync(id);
            if (!result)
            {
                return NotFound("Order Detail is not found");
            }
            return Ok("Order Detail is removed successfully");
        }

        //UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderDetil(int id, UpdateOrderDetailRequest orderDatail)
        {
            var updatedOrderDetail = await _orderDetailServices.UpdateOrderDetailAsync(id, orderDatail);
            if (updatedOrderDetail == null)
            {
                return NotFound($"Order Detail with id: {id} does not exist in the database");
            }
            return Ok(updatedOrderDetail);
        }   }
}
