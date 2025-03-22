using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("Users")]
	public class User
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Gender { get; set; }
		public string Email { get; set; }
		public string PasswordHash { get; set; }
		public string PhoneNumber { get; set; }
		public string Address { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
	}
}
