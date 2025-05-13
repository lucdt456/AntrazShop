using AntrazShop.Data;
using AntrazShop.Interfaces.Repositories;
using AntrazShop.Interfaces.Services;
using AntrazShop.Models;
using AntrazShop.Models.DTOModels;

namespace AntrazShop.Services
{
	public class ProductCCService : IProductCCService
	{
		private readonly IProductColorCapacityRepository _productCCRepository;

		public ProductCCService(IProductColorCapacityRepository productCCRepository)
		{
			_productCCRepository = productCCRepository;
		}

		public async Task<ServiceResponse<ColorCapacity>> EditColorCapacity(string productFolder, int id, ProductColorCapacityDTO dTO)
		{
			var response = new ServiceResponse<ColorCapacity>();
			try
			{
				var productCC = await _productCCRepository.GetProductCC(id);
				string oldImageName = productCC.Image;

				string oldImageUrl = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin", "imgs", "product",productFolder, oldImageName);

				string newImageName = ""; 
				if (dTO.Image == null)
				{
					newImageName = FileNameHelper.ToSlug(dTO.ColorName) + '_' + FileNameHelper.ToSlug(dTO.CapacityValue) + Path.GetExtension(oldImageName);

					string newImageUrl = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin", "imgs", "product", productFolder, newImageName);
					try
					{
						File.Move(oldImageUrl, newImageUrl);
					}
					catch
					{
						response.IsSuccess = false;
						response.Errors.Add("Lỗi khi đổi tên file ảnh");
					}
				}
				else
				{
					newImageName = FileNameHelper.ToSlug(dTO.ColorName) + '_' + FileNameHelper.ToSlug(dTO.CapacityValue) + Path.GetExtension(dTO.Image.FileName);

					string newImageUrl = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin", "imgs", "product", productFolder, newImageName);

					try
					{
						File.Delete(oldImageUrl);
						using(var stream = new FileStream(newImageUrl, FileMode.Create))
						{
							await dTO.Image.CopyToAsync(stream);
						}
					}
					catch
					{
						response.IsSuccess = false;
						response.Errors.Add("Lỗi khi thay thế file ảnh");
					}
				}
				var colorIdUpdate = await _productCCRepository.AddColor(dTO.ColorName);

				var capacityIdUpdate = await _productCCRepository.AddCapacity(dTO.CapacityValue);

				var colorCapacityUpdate = new ColorCapacity
				{
					Stock = dTO.Stock,
					Price = dTO.Price,
					ColorId = colorIdUpdate,
					CapacityId = capacityIdUpdate,
					Status = dTO.Status,
					Image = newImageName
				};

				response.Data = await _productCCRepository.EditColorCapacity(id, colorCapacityUpdate);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add("Lỗi: " + ex.Message);
			}
			return response;
		}
	}
}

