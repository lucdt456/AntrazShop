using AntrazShop.Data;

namespace AntrazShop.Models.ViewModels
{
	public class ProductColorCapacityVM
	{
		public int Id { get; set; }
		public string ColorName { get; set; }
		public string CapacityValue { get; set; }
		public string Image { get; set; }
		public Decimal Price { get; set; }
		public int Stock { get; set; }
		public int Status { get; set; }
		public List<ProductReviewVM> Reviews { get; set; } = new();
	}
}
