using System.ComponentModel.DataAnnotations;

namespace CodeBuddies_PizzaAPI.DTOs
{
    public class PatchCustomerRequest
    {
            [Required]
            public int Id { get; set; }
            public string? Email { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? Address { get; set; }
            public string? Phone { get; set; }
    }
}
