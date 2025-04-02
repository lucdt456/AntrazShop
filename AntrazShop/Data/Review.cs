using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("Reviews")]
	public class Review
	{
		[ForeignKey("User")]
		public int UserId { get; set; }
		public User User { get; set; }
		
		[ForeignKey("Color")]
		public int ColorId { get; set; }
		public Color Color { get; set; }

		public string Content { get; set; }
		public double Rating { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
	}
}
