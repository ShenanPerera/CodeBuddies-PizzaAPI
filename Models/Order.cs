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

        //TODO: create a testcontroller and test services with a DTO to post order details until actual controller , service available.
        public ICollection<OrderDetail> OrderDetails { get; set; }

        public decimal totalPrice { get; set; }

    }
}
