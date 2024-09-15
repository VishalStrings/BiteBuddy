using BiteBuddy.Web.Models;

namespace BiteBuddy.Web.Service.IService
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetCouponByIdAsync(int id);
        Task<ResponseDto?> GetAllCouponAsync();
        Task<ResponseDto?> GetCouponByCodeAsync(string couponCode);
        Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto);
        Task<ResponseDto?> UpdateCouponAsync(int id);
        Task<ResponseDto?> DeleteCouponAsync(int id);
    }
}
