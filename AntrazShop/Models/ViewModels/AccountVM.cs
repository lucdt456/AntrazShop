namespace AntrazShop.Models.ViewModels
{
	public class AccountVM
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Gender { get; set; }
		public string Avatar { get; set; }
		public DateTime CreateAt { get; set; }
		public List<string> Roles { get; set; }
	}
}
