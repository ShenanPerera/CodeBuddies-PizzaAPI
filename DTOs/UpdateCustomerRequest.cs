namespace CodeBuddies_PizzaAPI.DTOs
{
	public class UpdateCustomerRequest
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
	}

	public class UpdateCustomerEmailRequest
	{
		public int Id { get; set; }
		public string Email { get; set; }
	}
	public class UpdateCustomerFirstNameRequest
	{
		public int Id { get; set; }
		public string FirstName { get; set; }

	}

	public class UpdateCustomerLastLastRequest
	{
		public int Id { get; set; }
		public string LastName { get; set; }
	}

	public class UpdateCustomerPhoneRequest
	{
		public int Id { get; set; }
		public string Phone { get; set; }
	}

	public class UpdateCustomerAddressRequest
	{
		public int Id { get; set; }

		public string Address { get; set; }
	}
}
