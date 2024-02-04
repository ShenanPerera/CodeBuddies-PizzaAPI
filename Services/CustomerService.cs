using CodeBuddies_PizzaAPI.Data;
using CodeBuddies_PizzaAPI.DTOs;
using CodeBuddies_PizzaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeBuddies_PizzaAPI.Services
{
	public interface ICustomerService
	{
		Task<Customer> AddCustomerAsync(CreateCustomerRequest customer);
		Task<IEnumerable<Customer>> GetAllCustomersAsync();
		Task<Customer> GetCustomerByIdAsync(int id);
		Task<Customer> UpdateCustomerAsync(int id, UpdateCustomerRequest customer);
		Task<Customer> UpdateCustomerEmailAsync(int id, UpdateCustomerEmailRequest customer);
		Task<Customer> UpdateCustomerFirstNameAsync(int id, UpdateCustomerFirstNameRequest updateCustomerFirstNameRequest);
		Task<Customer> UpdateCustomerLastNameAsync(int id, UpdateCustomerLastNameRequest updateCustomerLastNameRequest);
		Task<Customer> UpdateCustomerAddressAsync(int id, UpdateCustomerAddressRequest updateCustomerAddressRequest);
		Task<Customer> UpdateCustomerPhoneAsync(int id, UpdateCustomerPhoneRequest updateCustomerPhoneRequest);
		Task<bool> DeleteCustomerAsync(int id);
	}

	public class CustomerService : ICustomerService
	{
		private readonly PizzaContext _context;

		public CustomerService(PizzaContext context)
		{
			_context = context;
		}

		public async Task<Customer> AddCustomerAsync(CreateCustomerRequest customer)
		{
			if (await _context.Customers.AnyAsync(a => a.Email == customer.Email))
			{
				throw new DbUpdateException("Customer already exists in the database.");
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

			return newCustomer;
		}

		public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
		{
			return await _context.Customers.ToListAsync();
		}

		public async Task<Customer> GetCustomerByIdAsync(int id)
		{
			return await _context.Customers.FindAsync(id);
		}

		public async Task<Customer> UpdateCustomerAsync(int id, UpdateCustomerRequest customer)
		{
			if (id != customer.Id)
			{
				throw new ArgumentException("The provided ID does not match the customer ID in the request body.");
			}

			var existingCustomer = await _context.Customers.FindAsync(id);

			if (existingCustomer == null)
			{
				throw new KeyNotFoundException($"Customer with id: {id} does not exist in the database.");
			}

			if (await _context.Customers.AnyAsync(c => c.Id != id && c.Email == customer.Email))
			{
				throw new DbUpdateException("The provided email already exists in another customer record.");
			}

			existingCustomer.Email = customer.Email;
			existingCustomer.FirstName = customer.FirstName;
			existingCustomer.LastName = customer.LastName;
			existingCustomer.Phone = customer.Phone;
			existingCustomer.Address = customer.Address;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!await _context.Customers.AnyAsync(a => a.Id == id))
				{
					throw new KeyNotFoundException($"Customer with id: {id} does not exist in the database.");
				}
				else
				{
					throw;
				}
			}

			return existingCustomer;
		}

		public async Task<bool> DeleteCustomerAsync(int id)
		{
			var customer = await _context.Customers.FindAsync(id);
			if (customer == null)
			{
				return false;
			}

			_context.Customers.Remove(customer);
			await _context.SaveChangesAsync();

			return true;
		}

		public async Task<Customer> UpdateCustomerEmailAsync(int id, UpdateCustomerEmailRequest customer)
		{
			if (id != customer.Id)
			{
				throw new ArgumentException("The provided ID does not match the customer ID in the request body.");
			}

			var existingCustomer = await _context.Customers.FindAsync(id);

			if (existingCustomer == null)
			{
				throw new KeyNotFoundException($"Customer with id: {id} does not exist in the database.");
			}

			if (await _context.Customers.AnyAsync(c => c.Id != id && c.Email == customer.Email))
			{
				throw new DbUpdateException("The provided email already exists in another customer record.");
			}

			existingCustomer.Email = customer.Email;
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!await _context.Customers.AnyAsync(a => a.Id == id))
				{
					throw new KeyNotFoundException($"Customer with id: {id} does not exist in the database.");
				}
				else
				{
					throw;
				}
			}

			return existingCustomer;
		}

		public async Task<Customer> UpdateCustomerFirstNameAsync(int id, UpdateCustomerFirstNameRequest customer)
		{
			if (id != customer.Id)
			{
				throw new ArgumentException("The provided ID does not match the customer ID in the request body.");
			}

			var existingCustomer = await _context.Customers.FindAsync(id);

			if (existingCustomer == null)
			{
				throw new KeyNotFoundException($"Customer with id: {id} does not exist in the database.");
			}

			existingCustomer.FirstName = customer.FirstName;
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!await _context.Customers.AnyAsync(a => a.Id == id))
				{
					throw new KeyNotFoundException($"Customer with id: {id} does not exist in the database.");
				}
				else
				{
					throw;
				}
			}
			return existingCustomer;
		}

		public async Task<Customer> UpdateCustomerLastNameAsync(int id, UpdateCustomerLastNameRequest customer)
		{
			if (id != customer.Id)
			{
				throw new ArgumentException("The provided ID does not match the customer ID in the request body.");
			}

			var existingCustomer = await _context.Customers.FindAsync(id);

			if (existingCustomer == null)
			{
				throw new KeyNotFoundException($"Customer with id: {id} does not exist in the database.");
			}

			existingCustomer.LastName = customer.LastName;
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!await _context.Customers.AnyAsync(a => a.Id == id))
				{
					throw new KeyNotFoundException($"Customer with id: {id} does not exist in the database.");
				}
				else
				{
					throw;
				}
			}
			return existingCustomer;
		}

		public async Task<Customer> UpdateCustomerAddressAsync(int id, UpdateCustomerAddressRequest customer)
		{
			if (id != customer.Id)
			{
				throw new ArgumentException("The provided ID does not match the customer ID in the request body.");
			}

			var existingCustomer = await _context.Customers.FindAsync(id);

			if (existingCustomer == null)
			{
				throw new KeyNotFoundException($"Customer with id: {id} does not exist in the database.");
			}

			existingCustomer.Address = customer.Address;
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!await _context.Customers.AnyAsync(a => a.Id == id))
				{
					throw new KeyNotFoundException($"Customer with id: {id} does not exist in the database.");
				}
				else
				{
					throw;
				}
			}
			return existingCustomer;
		}

		public async Task<Customer> UpdateCustomerPhoneAsync(int id, UpdateCustomerPhoneRequest customer)
		{
			if (id != customer.Id)
			{
				throw new ArgumentException("The provided ID does not match the customer ID in the request body.");
			}

			var existingCustomer = await _context.Customers.FindAsync(id);

			if (existingCustomer == null)
			{
				throw new KeyNotFoundException($"Customer with id: {id} does not exist in the database.");
			}

			existingCustomer.Phone = customer.Phone;
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!await _context.Customers.AnyAsync(a => a.Id == id))
				{
					throw new KeyNotFoundException($"Customer with id: {id} does not exist in the database.");
				}
				else
				{
					throw;
				}
			}
			return existingCustomer;
		}
	}
}