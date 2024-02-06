using CodeBuddies_PizzaAPI.Data;
using CodeBuddies_PizzaAPI.DTOs;
using CodeBuddies_PizzaAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace CodeBuddies_PizzaAPI.Services
{
    public interface IOrderServices
    {
        Task<Order> CreateOrderAsync(CreateOrder order);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();

        Task<Order> AddOrderDetailListtoOrders(int id);
        Task<Order> UpdateOnPreparation(int id);
        Task<Order> UpdateOrderFulFilled(int id);
        Task<bool> DeleteOrder(int id);
    }
    public class OrderService : IOrderServices
    {
        private readonly PizzaContext _context;
        private readonly ITestOrderDetailServices _testOrderDetailServices;
        public OrderService(PizzaContext context , ITestOrderDetailServices iTestOrderDetailServices) 
        { 
            _context = context;
            _testOrderDetailServices = iTestOrderDetailServices;
        }

        public async Task<Order> CreateOrderAsync(CreateOrder order)
        {
            var customer = await _context.Customers.FindAsync(order.CustomerId);

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
            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            decimal Total_Price = order.OrderDetails.Sum(od => od.Product.Price * od.Quantity);
            order.totalPrice = Total_Price;

            return order;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o=> o.Customer)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .ToListAsync();
        }

        public async Task<Order> AddOrderDetailListtoOrders(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                throw new ArgumentNullException("id");
            }

            order.OrderDetails = (ICollection<OrderDetail>)await _testOrderDetailServices.GetOrderDetailsByorderIDAsync(order.Id);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Orders.AnyAsync(a => a.Id == id))
                {
                    throw new KeyNotFoundException($"Order with id: {id} does not exist in the database.");
                }
                else
                {
                    throw;
                }
            }

            return order;
        }

        public async Task<Order> UpdateOnPreparation(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                throw new ArgumentNullException("id");
            }

            order.OnPreparation = true;
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<Order> UpdateOrderFulFilled(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                throw new ArgumentNullException("id");
            }

            order.OrderFullFilled = DateTime.Now;
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<bool> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                throw new ArgumentNullException("id");
            }

            if(order.OnPreparation == true)
            {
                throw new ArgumentException("Can't delete the order. Your order is on preparation");
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
