using AntrazShop.Helper;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;

namespace AntrazShop.Interfaces.Services
{
	public interface IShopService
	{
		Task<ServiceResponse<(IEnumerable<ProductVM>, Paginate)>> GetProducts(ProductFilter filter, int currentPg,
			int size);
	}
}
