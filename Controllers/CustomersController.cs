using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeBuddies_PizzaAPI.Data;
using CodeBuddies_PizzaAPI.Models;
using CodeBuddies_PizzaAPI.DTOs;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CodeBuddies_PizzaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly PizzaContext _context;

        public CustomersController(PizzaContext context)
        {
            _context = context;
        }

		// POST: api/Customers
		[HttpPost]
		public async Task<ActionResult<Customer>> PostCustomer(CraeteCustomerRequest customer)
		{
			try
			{
				if (_context.Customers.Any(a => a.Email == customer.Email))
				{
					return BadRequest("Customer already exists in the database");
				}

				var newCustomer = new Customer
				{
					Email = customer.Email,
					FirstName = customer.FirstName,
					LastName = customer.LastName,
					Address = customer.Address,
					Phone = customer.Phone,
				};

				_context.Customers.Add(newCustomer);
				await _context.SaveChangesAsync();

				// Return the newly created customer
				return CreatedAtAction("GetCustomer", new { id = newCustomer.Email }, newCustomer);
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
			var customers = await _context.Customers.ToListAsync();

            if (!customers.Any())
            {
                return NotFound("No customers found in the database");
            }

			return customers;
		}

        // GET: api/Customers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id )
        {
			var customer = await _context.Customers.FirstOrDefaultAsync(a => a.Id == id);

			if (customer == null)
            {
                return NotFound("Customer not found");
            }
            return customer;
        }


		// PUT: api/Customers/5
		[HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, UpdateCustomer customerUpdateModel)
        {
            if (id != customerUpdateModel.Id)
            {
                return BadRequest("The provided ID does not match the customer ID in the request body");
            }

            var existingCustomer = await _context.Customers.FindAsync(id);

			if (existingCustomer == null)
			{
				return NotFound($"Customer with id: {id} does not exist in the database");
			}

			if (_context.Customers.Any(c => c.Id != id && c.Email == customerUpdateModel.Email))
			{
				return BadRequest("The provided email already exists in another customer record");
			}

			existingCustomer.Email= customerUpdateModel.Email;
			existingCustomer.FirstName = customerUpdateModel.FirstName;
            existingCustomer.LastName = customerUpdateModel.LastName;
            existingCustomer.Phone = customerUpdateModel.Phone;
			existingCustomer.Address = customerUpdateModel.Address;

			try
			{
				await _context.SaveChangesAsync();
				return Ok(existingCustomer);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_context.Customers.Any(a => a.Id == id))
				{
					return NotFound($"Customer with id: {id} does not exist in the database");
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// DELETE: api/Customers/5
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound("Customer not found");
            }
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
			return Ok("Customer is removed successfully");
		}
    }
}
