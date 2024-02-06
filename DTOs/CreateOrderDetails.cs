using CodeBuddies_PizzaAPI.Models;

namespace CodeBuddies_PizzaAPI.DTOs
{
    public class CreateOrderDetails
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
    }
}
