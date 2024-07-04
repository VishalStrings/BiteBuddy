using BiteBuddy.Web.Models;
using BiteBuddy.Web.Service.IService;
using BiteBuddy.Web.Utility;

namespace BiteBuddy.Web.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;

        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.CouponAPIBase + "/api/CouponAPI/"

            });
        }

        public async Task<ResponseDto?> DeleteCouponAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.CouponAPIBase + "/api/CouponAPI/" + id
            });
        }

        public async Task<ResponseDto?> GetAllCouponAsync()
        {
           return await _baseService.SendAsync(new RequestDto()
           {
              ApiType = Utility.SD.ApiType.GET,
              Url = SD.CouponAPIBase + "/api/CouponAPI"


           });
        }

        public async Task<ResponseDto?> GetCouponAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/CouponAPI/" + id

            });
        }

        public async Task<ResponseDto?> GetCouponByCodeAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/CouponAPI/GetByCode/" + couponCode

            });
        }

        public async Task<ResponseDto?> UpdateCouponAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
