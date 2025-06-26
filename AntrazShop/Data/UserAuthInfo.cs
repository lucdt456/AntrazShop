using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	public class UserAuthInfo
	{
		[Key, ForeignKey("User")]
		public int UserId { get; set; }

		public User User { get; set; }

		[Required]
		public bool IsActive { get; set; } = true;

		[Required]
		[Range(0, int.MaxValue)]
		public int FailedAttempts { get; set; } = 5;

		public ICollection<LoginHistory> LoginHistories { get; set; } = new List<LoginHistory>();
	}
}
