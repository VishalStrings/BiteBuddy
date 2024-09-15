using BiteBuddy.Web.Models;
using BiteBuddy.Web.Service.IService;
using BiteBuddy.Web.Utility;

namespace BiteBuddy.Web.Service
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;

        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateProductAsync(ProductDto ProductDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ProductAPIBase + "/api/ProductAPI/",
                Data = ProductDto

            });
        }

        public async Task<ResponseDto?> DeleteProductAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ProductAPIBase + "/api/ProductAPI/" + id
            });
        }

        public async Task<ResponseDto?> GetAllProductAsync()
        {
           return await _baseService.SendAsync(new RequestDto()
           {
              ApiType = Utility.SD.ApiType.GET,
              Url = SD.ProductAPIBase + "/api/ProductAPI"


           });
        }

        public async Task<ResponseDto?> GetProductByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/ProductAPI/" + id

            });
        }

        public async Task<ResponseDto?> GetProductByCodeAsync(string ProductCode)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/ProductAPI/GetByCode/" + ProductCode

            });
        }

        public async Task<ResponseDto?> UpdateProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> UpdateProductAsync(ProductDto productDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> DeleteProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> DeleteProductByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}
