using BiteBuddy.Web.Models;
using System.Threading.Tasks;

namespace BiteBuddy.Web.Service.IService
{
    public interface IProductService
    {
        Task<ResponseDto?> GetAllProductsAsync();
        Task<ResponseDto?> GetProductByIdAsync(int id);
        Task<ResponseDto?> GetProductByCodeAsync(string code);
        Task<ResponseDto?> CreateProductAsync(ProductDto productDto);
        Task<ResponseDto?> UpdateProductAsync(ProductDto productDto);
        Task<ResponseDto?> DeleteProductByIdAsync(int id);
        Task<ResponseDto?> DeleteProductByCodeAsync(string code);

    }
}
