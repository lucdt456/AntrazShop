using AntrazShop.Data;
using AntrazShop.Models.ViewModels;
using AntrazShop.Repositories.Interfaces;
using AntrazShop.Services.Interfaces;

namespace AntrazShop.Services
{
	public class BrandService : IBrandService
	{
		private readonly IBrandRepository _brandRepository;
		public BrandService(IBrandRepository brandRepository)
		{
			_brandRepository = brandRepository;
		}

		public async Task CreateBrand(Brand brand)
		{
			await _brandRepository.CreateBrand(brand);
		}

		public async Task<bool> DeleteBrand(int id)
		{
			return await _brandRepository.DeleteBrand(id);
		}

		public async Task<BrandVM> GetBrand(int id)
		{
			return await _brandRepository.GetBrand(id);
		}

		public async Task<IEnumerable<BrandVM>> GetBrands()
		{
			return await _brandRepository.GetBrands();
		}

		public async Task<Brand> UpdateBrand(int id, Brand newBrand)
		{
			return await _brandRepository.UpdateBrand(id, newBrand);
		}
	}
}
