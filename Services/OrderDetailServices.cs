using CodeBuddies_PizzaAPI.Data;
using CodeBuddies_PizzaAPI.DTOs;
using CodeBuddies_PizzaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeBuddies_PizzaAPI.Services
{
    public interface IOrderDetailServices
    {
        Task<OrderDetail> AddOrderDetailAsync(OrderDetail orderDetail , int orderID, int productID);
        Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync();
        Task<OrderDetail> GetOrderDetailByIdAsync(int id);
        Task<OrderDetail> UpdateOrderDetailAsync(int id, UpdateOrderDetailRequest orderDetail);
        Task<bool> DeleteOrderDetailAsync(int id);
    }

    public class OrderDetailServices : IOrderDetailServices
    {
        private readonly PizzaContext _context;
        //_context is a private field to store the instance of PizzaContext
        public OrderDetailServices(PizzaContext context)
        {
            _context = context;
        }
        public async Task<OrderDetail> AddOrderDetailAsync(OrderDetail orderDetail, int orderID, int productID)
        {
            var order = await _context.Orders.FindAsync(orderID);
            if (order == null)
            {
                throw new ArgumentException("Provided orderId is invalid");
            }

            var product = await _context.Products.FindAsync(productID);
            if (product == null)
            {
                throw new ArgumentException("Provided productId is invalid");
            }

            var newOrderDetail = new OrderDetail
            {
                Quantity = orderDetail.Quantity,
                Order = order,
                Product = product,
                
            };
            _context.OrderDetails.Add(newOrderDetail);
            await _context.SaveChangesAsync();
            return newOrderDetail;
        }

        // public Task<bool> DeleteOrderDetailAsync(int id)
        //{
        //  throw new NotImplementedException();
        //}

        public async Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync()
        {
            return await _context.OrderDetails.ToListAsync();
        }

        public async Task<OrderDetail> GetOrderDetailByIdAsync(int id)
        {
            return await _context.OrderDetails.FindAsync(id);
        } 

        public async Task<bool> DeleteOrderDetailAsync(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return false;
            }

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<OrderDetail> UpdateOrderDetailAsync(int id, UpdateOrderDetailRequest orderDetail)
        {
            if (id != orderDetail.Id)
            {
                throw new ArgumentException("The provided ID does not match the order detail ID in the request body.");
            }

            var existingOrderDetail = await _context.OrderDetails.FindAsync(id);

            if (existingOrderDetail == null)
            {
                throw new KeyNotFoundException($"OrderDetail with id: {id} does not exist in the database.");
            }


            existingOrderDetail.Quantity = orderDetail.Quantity;
            existingOrderDetail.Order = orderDetail.Order;
            existingOrderDetail.Product = orderDetail.Product;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.OrderDetails.AnyAsync(a => a.Id == id))
                {
                    throw new KeyNotFoundException($"OrderDetail with id: {id} does not exist in the database.");
                }
                else
                {
                    throw;
                }
            }

            return existingOrderDetail;
        }



    }
}
