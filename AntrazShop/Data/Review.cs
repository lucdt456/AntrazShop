using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("Reviews")]
	public class Review
	{
		[Key]
		public int Id { get; set; }
		[ForeignKey("User")]
		public int UserId { get; set; }
		public User User { get; set; }

		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public Product Product { get; set; }
		public string Content { get; set; }
		public double Rating { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;

	}
}
