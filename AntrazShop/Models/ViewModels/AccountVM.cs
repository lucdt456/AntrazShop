namespace AntrazShop.Models.ViewModels
{
	public class AccountVM
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Gender { get; set; }
		public string? Address { get; set; }
		public DateOnly Birthday { get; set; }
		public string? Hometown { get; set; }
		public string Avatar { get; set; }
		public bool workerAccount { get; set; } = false;
		public DateTime CreateAt { get; set; }
		public List<RoleVM> Roles { get; set; }
		public bool isActive { get; set; }
	}
}
