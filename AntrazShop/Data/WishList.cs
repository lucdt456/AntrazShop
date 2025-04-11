using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("WishLists")]
	public class WishList
	{
		[ForeignKey("User")]
		public int UserId { get; set; }
		public User User { get; set; }

		[ForeignKey("ColorCapacity")]
		public int ColorCapacityId { get; set; }
		public ColorCapacity ColorCapacity { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;

	}
}
