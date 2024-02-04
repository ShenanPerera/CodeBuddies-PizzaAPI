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

		// PUT: api/Customers/email/5
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

		// PUT: api/Customers/first-name/5
		[HttpPut("first-name/{id}")]
		public async Task<IActionResult> PutCustomerFirstName(int id, UpdateCustomerFirstNameRequest customer)
		{
			var updatedCustomer = await _customerService.UpdateCustomerFirstNameAsync(id, customer);
			if (updatedCustomer == null)
			{
				return NotFound($"Customer with id: {id} does not exist in the database");
			}
			return Ok(updatedCustomer);
		}

		// PUT: api/Customers/last-name/5
		[HttpPut("last-name/{id}")]
		public async Task<IActionResult> PutCustomerLastName(int id, UpdateCustomerLastNameRequest customer)
		{
			var updatedCustomer = await _customerService.UpdateCustomerLastNameAsync(id, customer);
			if (updatedCustomer == null)
			{
				return NotFound($"Customer with id: {id} does not exist in the database");
			}
			return Ok(updatedCustomer);
		}

		// PUT: api/Customers/first-name/5
		[HttpPut("address/{id}")]
		public async Task<IActionResult> PutCustomerAddress(int id, UpdateCustomerAddressRequest customer)
		{
			var updatedCustomer = await _customerService.UpdateCustomerAddressAsync(id, customer);
			if (updatedCustomer == null)
			{
				return NotFound($"Customer with id: {id} does not exist in the database");
			}
			return Ok(updatedCustomer);
		}

		// PUT: api/Customers/first-name/5
		[HttpPut("phone/{id}")]
		public async Task<IActionResult> PutCustomerPhone(int id, UpdateCustomerPhoneRequest customer)
		{
			var updatedCustomer = await _customerService.UpdateCustomerPhoneAsync(id, customer);
			if (updatedCustomer == null)
			{
				return NotFound($"Customer with id: {id} does not exist in the database");
			}
			return Ok(updatedCustomer);
		}
	}
}