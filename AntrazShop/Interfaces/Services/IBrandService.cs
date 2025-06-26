using AntrazShop.Helper;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;

namespace AntrazShop.Interfaces.Services
{
	public interface IBrandService
	{
		Task<ServiceResponse<IEnumerable<BrandVM>>> GetBrands();
		Task<ServiceResponse<BrandVM>> GetBrand(int id);
		Task<ServiceResponse<BrandVM>> CreateBrand(BrandDTO dto);
		Task<ServiceResponse<BrandVM>> UpdateBrand(int id, BrandDTO dto);
		Task<ServiceResponse<string>> DeleteBrand(int id);
	}
}
