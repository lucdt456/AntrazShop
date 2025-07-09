using AntrazShop.Data;
using AntrazShop.Helper;
using AntrazShop.Interfaces.Repositories;
using AntrazShop.Interfaces.Services;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;
using AutoMapper;

namespace AntrazShop.Services
{
	public class ShopService : IShopService
	{
		private readonly IShopRepository _shopRepository;
		private readonly IMapper _mapper;
		public ShopService(IShopRepository shopRepository, IMapper mapper)
		{
			_shopRepository = shopRepository;
			_mapper = mapper;
		}

		public async Task<ServiceResponse<(IEnumerable<ProductVM>, Paginate)>> GetProducts(ProductFilter filter, int currentPg, int size)
		{
			var response = new ServiceResponse<(IEnumerable<ProductVM>, Paginate)>();
			try
			{
				var totalProducts = await _shopRepository.GetTotalProductCountFilter(filter);
				var pagination = new Paginate(totalProducts, currentPg, size);
				int recSkip = (currentPg - 1) * size;
				recSkip = (recSkip < 0) ? 0 : recSkip;

				var products = await _shopRepository.GetProducts(filter, recSkip, size);
				var productVMs = _mapper.Map<List<ProductVM>>(products);

				response.Data = (productVMs, pagination);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}
			return response;
		}

		public async Task<ServiceResponse<IEnumerable<ProductVM>>> GetProductsTopRating()
		{
			var response = new ServiceResponse<IEnumerable<ProductVM>>();
			try
			{
				var products = await _shopRepository.GetProductsTopRating();
				response.Data = _mapper.Map<List<ProductVM>>(products);
				
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}
			return response;
		}

		public async Task<ServiceResponse<IEnumerable<ProductVM>>> GetProductsTopSale()
		{
			var response = new ServiceResponse<IEnumerable<ProductVM>>();
			try
			{
				var products = await _shopRepository.GetProductsTopSale();
				response.Data = _mapper.Map<List<ProductVM>>(products);

			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}
			return response;
		}

		public async Task<ServiceResponse<IEnumerable<ProductVM>>> GetProductsTopSell()
		{
			var response = new ServiceResponse<IEnumerable<ProductVM>>();
			try
			{
				var products = await _shopRepository.GetProductsTopSell();
				response.Data = _mapper.Map<List<ProductVM>>(products);
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
