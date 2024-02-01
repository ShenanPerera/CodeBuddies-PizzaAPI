using System.ComponentModel.DataAnnotations;

namespace CodeBuddies_PizzaAPI.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime OrderPlaced { get; set; }
        public DateTime OrderFullFilled { get; set; }

        public bool OnPreparation { get; set; }
        public Customer Customer { get; set; }

        //TODO: until Order detail lists are ready . 
        //public ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
