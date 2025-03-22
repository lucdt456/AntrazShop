using AntrazShop.Data;

namespace AntrazShop.Services.Interfaces
{
	public interface IBrandService
	{
		Task<IEnumerable<Brand>> GetBrands();
		Task<Brand> GetBrand(int id);
		Task CreateBrand(Brand brand);
		Task<Brand> UpdateBrand(int id, Brand newBrand);
		Task<bool> DeleteBrand(int id);
	}
}
