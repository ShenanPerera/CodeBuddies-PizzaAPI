using System.ComponentModel.DataAnnotations;

namespace CodeBuddies_PizzaAPI.DTOs
{
	public class UpdateCustomerRequest
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		public string Phone { get; set; }
	}

	public class UpdateCustomerEmailRequest
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string Email { get; set; }
	}
	public class UpdateCustomerFirstNameRequest
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string FirstName { get; set; }

	}

	public class UpdateCustomerLastLastRequest
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string LastName { get; set; }
	}

	public class UpdateCustomerPhoneRequest
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string Phone { get; set; }
	}

	public class UpdateCustomerAddressRequest
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string Address { get; set; }
	}
}
