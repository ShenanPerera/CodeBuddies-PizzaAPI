using CodeBuddies_PizzaAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace CodeBuddies_PizzaAPI.DTOs
{
    public class CreateOrder
    {
        [Key]
        public int Id { get; set; }
        public DateTime OrderPlaced { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
