namespace AntrazShop.Models.DTOModels
{
	public class AccountDTO
	{
		public string Name { get; set; }
		public string Gender { get; set; }
		public string Email { get; set; }
		public string? Password { get; set; }
		public string PhoneNumber { get; set; }
		public IFormFile? Avatar { get; set; }
		public DateOnly Birthday { get; set; }
		public string Hometown { get; set; }
		public List<int>? Roles { get; set; }
		public string Address { get; set; }
	}
}
