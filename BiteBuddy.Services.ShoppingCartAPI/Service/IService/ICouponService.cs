using BiteBuddy.Services.ShoppingCartAPI.Models;
using BiteBuddy.Services.ShoppingCartAPI.Models.Dto;

namespace BiteBuddy.Services.ShoppingCartAPI.Service.IService
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon(string couponCode);
    }
}
