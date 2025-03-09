using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("WishLists")]
	public class WishList
	{
		[ForeignKey("User")]
		public int UserId { get; set; }
		public virtual User User { get; set; }

		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public virtual Product Product { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;

	}
}
