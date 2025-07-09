using AntrazShop.Data;
using AntrazShop.Helper;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;

namespace AntrazShop.Interfaces.Services
{
	public interface IShopService
	{
		Task<ServiceResponse<(IEnumerable<ProductVM>, Paginate)>> GetProducts(ProductFilter filter, int currentPg,
			int size);
		Task<ServiceResponse<IEnumerable<ProductVM>>> GetProductsTopSale();
		Task<ServiceResponse<IEnumerable<ProductVM>>> GetProductsTopSell();
		Task<ServiceResponse<IEnumerable<ProductVM>>> GetProductsTopRating();
	}
}
