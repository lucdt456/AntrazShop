using AntrazShop.Data;
using AntrazShop.Helper;
using AntrazShop.Interfaces.Repositories;
using AntrazShop.Interfaces.Services;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;
using AutoMapper;

namespace AntrazShop.Services
{
	public class BrandService : IBrandService
	{
		private readonly IBrandRepository _brandRepository;
		private readonly IMapper _mapper;
		public BrandService(IBrandRepository brandRepository, IMapper mapper)
		{
			_brandRepository = brandRepository;
			_mapper = mapper;
		}

		public async Task<ServiceResponse<BrandVM>> CreateBrand(BrandDTO dto)
		{
			var response = new ServiceResponse<BrandVM>();
			try
			{
				var brand = _mapper.Map<Brand>(dto); 

				brand = await _brandRepository.CreateBrand(brand);

				response.Data = _mapper.Map<BrandVM>(brand);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}
			return response;
		}

		public async Task<ServiceResponse<string>> DeleteBrand(int id)
		{
			var response = new ServiceResponse<string>();
			try
			{
				await _brandRepository.DeleteBrand(id);
				response.Data = "Xoá thành công!";
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}
			return response;
		}

		public async Task<ServiceResponse<BrandVM>> GetBrand(int id)
		{
			var response = new ServiceResponse<BrandVM>();
			try
			{
				var brand = await _brandRepository.GetBrand(id);
				response.Data = _mapper.Map<BrandVM>(brand);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}
			return response;
		}

		public async Task<ServiceResponse<IEnumerable<BrandVM>>> GetBrands()
		{
			var response = new ServiceResponse<IEnumerable<BrandVM>> ();
			try
			{
				var brands = await _brandRepository.GetBrands();
				response.Data = _mapper.Map<List<BrandVM>>(brands);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}
			return response;
		}

		public async Task<ServiceResponse<BrandVM>> UpdateBrand(int id, BrandDTO dto)
		{
			var response = new ServiceResponse<BrandVM>();
			try
			{
				var brand = _mapper.Map<Brand>(dto);
				brand.Id = id;
				brand = await _brandRepository.UpdateBrand(brand);
				response.Data = _mapper.Map<BrandVM>(brand);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}
			return response;
		}
	}
}
