using CodeBuddies_PizzaAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace CodeBuddies_PizzaAPI.DTOs
{
    public class UpdateOrderDetailRequest
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Order Order { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
