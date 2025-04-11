namespace AntrazShop.Models.DTOModels
{
	public class ProductDTO
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public int BrandId { get; set; }
		public int CategoryId { get; set; }
		public string ImageView { get; set; }
		public decimal DiscountAmount { get; set; }

		public List<ProductColorCapacityDTO> ProductCCDTOs { get; set; }
	}
}
