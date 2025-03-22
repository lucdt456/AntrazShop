using AntrazShop.Data;

namespace AntrazShop.Repositories.Interfaces
{
	public interface IBrandRepository
	{
		Task<IEnumerable<Brand>> GetBrands();
		Task<Brand> GetBrand(int id);
		Task CreateBrand(Brand brand);
		Task<Brand> UpdateBrand(int id, Brand newBrand);
		Task<bool> DeleteBrand(int id);
	}
}
