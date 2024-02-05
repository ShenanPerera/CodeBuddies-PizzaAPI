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
        Task<bool> DeleteCustomerAsync(int id);
        Task<Customer> PatchCustomerAsync(PatchCustomerRequest patchRequest);
        Task<Customer> UpdateCustomerEmailAsync(int id, UpdateCustomerEmailRequest customer);
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

        public async Task<Customer> PatchCustomerAsync(PatchCustomerRequest patchRequest)
        {
            var existingCustomer = await _context.Customers.FindAsync(patchRequest.Id);

            if (existingCustomer == null)
            {
                throw new KeyNotFoundException($"Customer with id: {patchRequest.Id} does not exist in the database.");
            }

            // Apply partial updates
            if (!string.IsNullOrEmpty(patchRequest.Email))
            {
                if (await _context.Customers.AnyAsync(c => c.Id != patchRequest.Id && c.Email == patchRequest.Email))
                {
                    throw new DbUpdateException("The provided email already exists in another customer record.");
                }
                existingCustomer.Email = patchRequest.Email;
            }

            if (!string.IsNullOrEmpty(patchRequest.FirstName))
            {
                existingCustomer.FirstName = patchRequest.FirstName;
            }

            if (!string.IsNullOrEmpty(patchRequest.LastName))
            {
                existingCustomer.LastName = patchRequest.LastName;
            }

            if (!string.IsNullOrEmpty(patchRequest.Phone))
            {
                existingCustomer.Phone = patchRequest.Phone;
            }

            if (!string.IsNullOrEmpty(patchRequest.Address))
            {
                existingCustomer.Address = patchRequest.Address;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Customers.AnyAsync(a => a.Id == patchRequest.Id))
                {
                    throw new KeyNotFoundException($"Customer with id: {patchRequest.Id} does not exist in the database.");
                }
                else
                {
                    throw;
                }
            }

            return existingCustomer;
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

    }
}