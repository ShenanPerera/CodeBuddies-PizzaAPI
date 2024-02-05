using CodeBuddies_PizzaAPI.DTOs;
using CodeBuddies_PizzaAPI.Models;
using CodeBuddies_PizzaAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CodeBuddies_PizzaAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomersController : ControllerBase
	{
		private readonly ICustomerService _customerService;

		public CustomersController(ICustomerService customerService)
		{
			_customerService = customerService;
		}

		// POST: api/Customers
		[HttpPost]
		public async Task<ActionResult<Customer>> PostCustomer(CreateCustomerRequest customer)
		{
			try
			{
				var newCustomer = await _customerService.AddCustomerAsync(customer);
				return CreatedAtAction("GetCustomer", new { id = newCustomer.Id }, newCustomer);
			}
			catch (DbUpdateConcurrencyException e)
			{
				return BadRequest(e.Message);
			}
		}

		// GET: api/Customers
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
		{
			var customers = await _customerService.GetAllCustomersAsync();
			if (!customers.Any())
			{
				return NotFound("No customers found in the database");
			}
			return Ok(customers);
		}

		// GET: api/Customers/{id}
		[HttpGet("{id}")]
		public async Task<ActionResult<Customer>> GetCustomer(int id)
		{
			var customer = await _customerService.GetCustomerByIdAsync(id);
			if (customer == null)
			{
				return NotFound("Customer not found");
			}
			return customer;
		}

		// DELETE: api/Customers/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCustomer(int id)
		{
			var result = await _customerService.DeleteCustomerAsync(id);
			if (!result)
			{
				return NotFound("Customer not found");
			}
			return Ok("Customer is removed successfully");
		}

		// PUT: api/Customers/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutCustomer(int id, UpdateCustomerRequest customer)
		{
			var updatedCustomer = await _customerService.UpdateCustomerAsync(id, customer);
			if (updatedCustomer == null)
			{
				return NotFound($"Customer with id: {id} does not exist in the database");
			}
			return Ok(updatedCustomer);
		}

		//// PUT: api/Customers/email/5
		[HttpPut("email/{id}")]
		public async Task<IActionResult> PutCustomerEmail(int id, UpdateCustomerEmailRequest customer)
		{
			var updatedCustomer = await _customerService.UpdateCustomerEmailAsync(id, customer);
			if (updatedCustomer == null)
			{
				return NotFound($"Customer with id: {id} does not exist in the database");
			}
			return Ok(updatedCustomer);
		}

		// PATCH: api/Customers/5
		[HttpPatch("{id}")]
        public async Task<IActionResult> PatchCustomer(int id, [FromBody] PatchCustomerRequest patchRequest)
        {
            try
            {
                // You might want to set the id in the patchRequest before passing it to the service.
                patchRequest.Id = id;

                var patchedCustomer = await _customerService.PatchCustomerAsync(patchRequest);
                return Ok(patchedCustomer);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (DbUpdateException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


    }
}