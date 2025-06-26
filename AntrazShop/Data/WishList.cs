using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("WishLists")]
	public class WishList
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[ForeignKey("User")]
		public int UserId { get; set; }

		public User User { get; set; }

		[Required]
		[ForeignKey("ColorCapacity")]
		public int ColorCapacityId { get; set; }

		public ColorCapacity ColorCapacity { get; set; }

		[Required]
		public DateTime CreatedAt { get; set; } = DateTime.Now;
	}
}
