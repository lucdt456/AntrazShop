using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AntrazShop.Data
{
	public class LoginHistory
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[ForeignKey("UserAuthInfo")]
		public int UserId { get; set; }

		public UserAuthInfo UserAuthInfo { get; set; }

		[Required]
		[StringLength(50)]
		public string StatusLogin { get; set; }

		[Required]
		[StringLength(100)]
		public string IPAddress { get; set; }

		[Required]
		public DateTime Time { get; set; }
	}
}
