using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("EmailCodes")]
	public class EmailCode
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[ForeignKey("User")]
		public int UserId { get; set; }
		public User User { get; set; }
		public string Code { get; set; }  
		public DateTime CreatedAt { get; set; } 
		public DateTime ExpiresAt { get; set; }  
		public bool IsUsed { get; set; }
	}
}
