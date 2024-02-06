using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CodeBuddies_PizzaAPI.DTOs
{
    public class ProductResponseDTO
    {
        
        public int Id { get; set; }
        public string Name { get; set; } = null!;

       
        public decimal Price { get; set; }





    }
}
