using System.ComponentModel.DataAnnotations;

namespace CodeBuddies_PizzaAPI.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
