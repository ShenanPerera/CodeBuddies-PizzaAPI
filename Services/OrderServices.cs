using CodeBuddies_PizzaAPI.Data;
using CodeBuddies_PizzaAPI.DTOs;
using CodeBuddies_PizzaAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace CodeBuddies_PizzaAPI.Services
{
    public interface IOrderServices
    {
        Task<Order> CreateOrderAsync(CreateOrder order , int customerID);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
    }
    public class OrderService : IOrderServices
    {
        private readonly PizzaContext _context;
        public OrderService(PizzaContext context) 
        { 
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(CreateOrder order , int customerID)
        {
            var customer = await _context.Customers.FindAsync(customerID);

            if (customer == null)
            {
                throw new ArgumentException("The provided customer ID is not valid");
            }

            var newOrder = new Order
            {
                OrderPlaced = DateTime.Now,
                OrderFullFilled = DateTime.MinValue,
                OnPreparation = false,
                Customer = customer
            };

            Console.WriteLine(newOrder);

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return newOrder;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders.FindAsync(orderId);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }
    }
}
