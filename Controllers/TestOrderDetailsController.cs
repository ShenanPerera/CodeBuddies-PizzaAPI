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
    public class TestOrderDetailsController : ControllerBase
    {
        private readonly ITestOrderDetailServices _orderDetailServices;

        public TestOrderDetailsController(ITestOrderDetailServices orderDetailServices)
        {
            _orderDetailServices = orderDetailServices;
        }

        //P
        [HttpPost]
        public async Task<ActionResult<OrderDetail>> PostOrder(CreateOrderDetails orderDetail)
        {
            try
            {
                var newOrderDetail = await _orderDetailServices.AddOrderDetails(orderDetail);
                return CreatedAtAction("GetOrderDetail", new { id = newOrderDetail.Id }, newOrderDetail);
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            }
        }

        //GET : api/OrderDetails/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetail(int id)
        {
            var orderDetail = await _orderDetailServices.GetOrderDetailById(id);

            if (orderDetail.Order == null && orderDetail.Product == null)
            {
                return NotFound();
            }

            if (orderDetail == null)
            {
                return BadRequest("Order not found");
            }
            return orderDetail;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetailsByOrderID(int id)
        {
            var orderDetailList = await _orderDetailServices.GetOrderDetailsByorderIDAsync(id);

            if (!orderDetailList.Any())
            {
                return NotFound("No orders found in the database");
            }

            return Ok(orderDetailList);
        }
    }
}
