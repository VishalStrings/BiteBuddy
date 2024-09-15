using BiteBuddy.Services.ShoppingCartAPI.Models.Dto;

namespace BiteBuddy.Services.ShoppingCartAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
