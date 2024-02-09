using CodeBuddies_PizzaAPI.Data;
using CodeBuddies_PizzaAPI.DTOs;
using CodeBuddies_PizzaAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CodeBuddies_PizzaAPI.Services
{
    public interface ITestOrderDetailServices
    {
        Task<OrderDetail> AddOrderDetails(CreateOrderDetails od);
        Task<OrderDetail> GetOrderDetailById(int id);
        Task<IEnumerable<OrderDetail>> GetOrderDetailsByorderIDAsync(int id);
    }
    public class TestOrderDetailServices : ITestOrderDetailServices
    {
        private readonly PizzaContext _context;

        public TestOrderDetailServices(PizzaContext context)
        {
            _context = context;
        }

        public async Task<OrderDetail> AddOrderDetails(CreateOrderDetails od)
        {
            var order = await _context.Orders.FindAsync(od.OrderID);
            if (order == null)
            {
                throw new ArgumentException("The order ID does not match the order ID in the request body");
            }

            var product = await _context.Products.FindAsync(od.ProductID);
            if (product == null)
            {
                throw new ArgumentException("The product ID does not match the product ID in the request body.");
            }

            var newOrderDetail = new OrderDetail
            {
                Quantity = od.Quantity,
                Order = order,
                Product = product,
            };

            _context.OrderDetails.Add(newOrderDetail);
            await _context.SaveChangesAsync();

            return newOrderDetail;
        }

        public async Task<OrderDetail> GetOrderDetailById(int id)
        {
            return await _context.OrderDetails.Include(od => od.Order).Include(od => od.Product).FirstOrDefaultAsync(od => od.Id == id);
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByorderIDAsync(int id)
        {
            var orderDetailList = await _context.OrderDetails.Include(od => od.Order).ThenInclude(od => od.Customer).Include(od => od.Product).Where(od => od.Order.Id == id).ToListAsync();

            if(orderDetailList == null)
            {
                throw new ArgumentNullException();
            }

            return orderDetailList;
        }
    }
}
