namespace AntrazShop.Models.DTOModels
{
	public class ProductDTO
	{
		public string Name { get; set; }
		public Decimal Price { get; set; }
		public Decimal DiscountAmount { get; set; }
		public string Description { get; set; }
		public string ImageView { get; set; }
		public int BrandId { get; set; }
		public int CategoryId { get; set; }
		public int status { get; set; }
		public int Stock { get; set; }
	}
}
