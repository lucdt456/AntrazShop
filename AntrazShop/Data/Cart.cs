using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntrazShop.Data
{
	[Table("Carts")]
	public class Cart
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
		[Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
		public int Quantity { get; set; }

		[Required]
		[Range(0, double.MaxValue, ErrorMessage = "Tổng tiền không được âm")]
		[Column(TypeName = "decimal(18,2)")] 
		public double Total { get; set; }
	}
}
