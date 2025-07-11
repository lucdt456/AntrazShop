using AntrazShop.Data;
using AntrazShop.Helper;
using AntrazShop.Interfaces.Repositories;
using AntrazShop.Interfaces.Services;
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
				string oldImageUrl = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin", "imgs", "product", productFolder, oldImageName);

				// Kiểm tra có thay đổi màu/capacity không
				bool isColorChanged = FileNameHelper.ToSlug(productCC.Color.NameColor) != FileNameHelper.ToSlug(dTO.ColorName);
				bool isCapacityChanged = FileNameHelper.ToSlug(productCC.Capacity.Value) != FileNameHelper.ToSlug(dTO.CapacityValue);

				if (isColorChanged || isCapacityChanged)
				{
					// Kiểm tra trùng lặp
					var productCCs = await _productCCRepository.GetProductCCsFromCCid(id);
					var checkExistCC = productCCs.Any(cc =>
						FileNameHelper.ToSlug(cc.Color.NameColor) == FileNameHelper.ToSlug(dTO.ColorName) &&
						FileNameHelper.ToSlug(cc.Capacity.Value) == FileNameHelper.ToSlug(dTO.CapacityValue));

					if (checkExistCC)
					{
						response.IsSuccess = false;
						response.Errors.Add("Phân loại sản phẩm đã tồn tại!");
						return response;
					}

					string newImageName = "";
					if (dTO.Image == null)
					{
						newImageName = FileNameHelper.ToSlug(dTO.ColorName) + '_' + FileNameHelper.ToSlug(dTO.CapacityValue) + $"{DateTime.Now:yyyyMMddHHmmss}" + Path.GetExtension(oldImageName);
						string newImageUrl = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin", "imgs", "product", productFolder, newImageName);

						File.Move(oldImageUrl, newImageUrl);
					}
					else
					{
						newImageName = FileNameHelper.ToSlug(dTO.ColorName) + '_' + FileNameHelper.ToSlug(dTO.CapacityValue) + $"{DateTime.Now:yyyyMMddHHmmss}" + Path.GetExtension(dTO.Image.FileName);
						string newImageUrl = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin", "imgs", "product", productFolder, newImageName);

						File.Delete(oldImageUrl);
						using (var stream = new FileStream(newImageUrl, FileMode.Create))
						{
							await dTO.Image.CopyToAsync(stream);
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
				else
				{
					string finalImageName = oldImageName;

					if (dTO.Image != null)
					{
						// Có ảnh mới, thay thế
						finalImageName = Path.GetFileNameWithoutExtension(oldImageName) + $"{DateTime.Now:yyyyMMddHHmmss}" + Path.GetExtension(dTO.Image.FileName);
						string newImageUrl = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin", "imgs", "product", productFolder, finalImageName);

						File.Delete(oldImageUrl);
						using (var stream = new FileStream(newImageUrl, FileMode.Create))
						{
							await dTO.Image.CopyToAsync(stream);
						}
					}

					var colorCapacityUpdate = new ColorCapacity
					{
						Stock = dTO.Stock,
						Price = dTO.Price,
						ColorId = productCC.ColorId,
						CapacityId = productCC.CapacityId,
						Status = dTO.Status,
						Image = finalImageName
					};

					response.Data = await _productCCRepository.EditColorCapacity(id, colorCapacityUpdate);
				}

				response.IsSuccess = true;
				return response;
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add("Lỗi không cập nhật được sản phẩm: " + ex.Message);
				return response;
			}
		}

		public async Task<ServiceResponse<ColorCapacity>> CreateColorCapacity(int idProduct, string productFolder, ProductColorCapacityDTO dTO)
		{
			var response = new ServiceResponse<ColorCapacity>();
			try
			{
				var productCCs = await _productCCRepository.GetProductCCsFromProductId(idProduct);
				var checkExistCC = false;
				foreach (var cc in productCCs)
				{
					if (FileNameHelper.ToSlug(cc.Color.NameColor) == FileNameHelper.ToSlug(dTO.ColorName)
					&& FileNameHelper.ToSlug(cc.Capacity.Value) == FileNameHelper.ToSlug(dTO.CapacityValue))
					{
						checkExistCC = true;
					}
				}

				if (!checkExistCC)
				{
					var imageFileName = FileNameHelper.ToSlug(dTO.ColorName) + $"{DateTime.Now:yyyyMMddHHmmss}" + '_' + FileNameHelper.ToSlug(dTO.CapacityValue) + Path.GetExtension(dTO.Image.FileName);
					var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin", "imgs", "product", productFolder, imageFileName);

					try
					{
						using (var stream = new FileStream(imagePath, FileMode.Create))
						{
							await dTO.Image.CopyToAsync(stream);
						}
					}
					catch
					{
						response.IsSuccess = false;
						response.Errors.Add("Không thể lưu hình ảnh sản phẩm");
						return response;
					}

					var colorId = await _productCCRepository.AddColor(dTO.ColorName);

					var capacityId = await _productCCRepository.AddCapacity(dTO.CapacityValue);

					var newCC = new ColorCapacity
					{
						Stock = dTO.Stock,
						Price = dTO.Price,
						ColorId = colorId,
						CapacityId = capacityId,
						ProductId= idProduct,
						Status = dTO.Status,
						Image = imageFileName,
						CreateAt = DateTime.Now
					};
					try
					{
						response.Data = await _productCCRepository.AddColorCapacity(newCC);
					}
					catch
					{
						response.IsSuccess = false;
						response.Errors.Add("Lỗi khi thêm sản phẩm vào Database");
						return response;
					}
					return response;
				}
				else
				{
					response.IsSuccess = false;
					response.Errors.Add("Phân loại sản phẩm đã tồn tại!");
					return response;
				}
				
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add("Lỗi: " + ex.Message);
				return response;
			}	
		}

		public async Task<ServiceResponse<bool>> DeleteColorCapacity(int id, string productFolder)
		{
			var response = new ServiceResponse<bool>();

			try
			{
				var productCC = await _productCCRepository.GetProductCC(id);
				string urlImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin", "imgs", "product", productFolder, productCC.Image);
				try
				{
					File.Delete(urlImage);
				}
				catch
				{
					response.IsSuccess = false;
					response.Errors.Add("Lỗi khi xoá file ảnh");
				}

				await _productCCRepository.DeleteProductCC(id);
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

