using AntrazShop.Data;
using AntrazShop.Models.DTOModels;

namespace AntrazShop.Interfaces.Repositories
{
	public interface IShopRepository
	{
		Task<IEnumerable<Product>> GetProducts(ProductFilter filter, int skip, int take);

		Task<int> GetTotalProductCountFilter(ProductFilter filter);
		Task<IEnumerable<Product>> GetProductsTopSale ();
		Task<IEnumerable<Product>> GetProductsTopSell();
		Task<IEnumerable<Product>> GetProductsTopRating();
	}
}
