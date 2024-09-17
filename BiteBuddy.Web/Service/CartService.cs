using BiteBuddy.Web.Models;
using BiteBuddy.Web.Service.IService;
using BiteBuddy.Web.Utility;
using static BiteBuddy.Web.Utility.SD;
using System;

namespace BiteBuddy.Web.Service
{
    public class CartService : ICartService
    {
        private readonly IBaseService _baseService;

        public CartService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> GetCartByUserIdAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.ShoppingCartAPIBase + "/api/cart/GetCart/"+userId
            });
        }

        public async Task<ResponseDto?> UpsertCartAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ShoppingCartAPIBase + "/api/cart/CartUpsert",
                Data = cartDto
            });
        }

        public async Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ShoppingCartAPIBase + "/api/cart/ApplyCoupon",
                Data = cartDto

            });
        }

        public async Task<ResponseDto?> RemoveCartAsync(int cartDetailsId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data=cartDetailsId,
                Url = SD.ShoppingCartAPIBase + "/api/cart/RemoveCart/" + cartDetailsId
            });
        }
    }
}
