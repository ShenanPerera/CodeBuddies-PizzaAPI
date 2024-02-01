using CodeBuddies_PizzaAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace CodeBuddies_PizzaAPI.DTOs
{
    public class CreateOrder
    {
        public DateTime OrderPlaced { get; set; }

        public DateTime orderFullFilled { get; set; }

        public bool OnPreparation { get; set; }
        public Customer CustomerId { get; set; }
    }
}
