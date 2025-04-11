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
		
		[ForeignKey("ColorCapacity")]
		public int ColorCapacityId { get; set; }
		public ColorCapacity ColorCapacity { get; set; }

		public string Description { get; set; }
		public double Rating { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
	}
}
