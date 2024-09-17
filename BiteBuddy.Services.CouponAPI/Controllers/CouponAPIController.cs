using AutoMapper;
using BiteBuddy.Services.CouponAPI.Data;
using BiteBuddy.Services.CouponAPI.Models;
using BiteBuddy.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BiteBuddy.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/coupon")]
    [ApiController]
    //[Authorize]
    public class CouponAPIController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly ResponseDto _response;
        private readonly IMapper _mapper;

        public CouponAPIController(ApplicationDBContext dBContext, ResponseDto response, IMapper mapper) // Dependency Injection
        {
            this._dbContext = dBContext;
            this._mapper = mapper;
            this._response = response;
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
               IEnumerable<Coupon> CouponList = _dbContext.Coupons.ToList();
               _response.Result =  CouponList;
            }

            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto GetById(int id)
        {
            try
            {
                Coupon couponList = _dbContext.Coupons.FirstOrDefault(x=>x.Id==id);
                _response.Result = _mapper.Map<CouponDto>(couponList);
            }

            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                Coupon coupon = _dbContext.Coupons.FirstOrDefault(x => x.CouponCode.ToLower() == code.ToLower());
                if (coupon == null)
                {
                    _response.IsSuccess=false;
                    _response.Result = NotFound(new {message = "Coupon not found."});
                }
                 _response.Result = _mapper.Map<CouponDto>(coupon);

                ////#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                //                Coupon CouponList = _dbContext.Coupons.FirstOrDefault(predicate: x => x.CouponCode == couponCode);
                //#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                //                return CouponList;
            }

            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        [HttpPost]
        //[Authorize(Roles = "ADMIN")]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _dbContext.Coupons.Add(obj);
                _dbContext.SaveChanges();
                _response.Result = _mapper.Map<CouponDto>(obj);
            }

            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;

            }

            return _response;
        }

        [HttpPut]
        //[Authorize(Roles = "ADMIN")]
        public ResponseDto Put(int id,[FromBody] CouponDto couponDto)
        {
            try
            {
                if(couponDto == null)
                {
                    _response.Result =  NotFound(new {});
                }
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                // obj = _dbContext.Coupons.First(x => x.Id == id);
                //if(obj == null)
                //{
                //    _response.Result = NotFound(new { });
                //}
               
                _dbContext.Coupons.Update(obj);
                _dbContext.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(obj);

            }

            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;

            }

            return _response;
        }

        [HttpPost("RemoveCart")]
        //[Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Coupon obj = _dbContext.Coupons.First(x => x.Id == id);
                if(obj != null)
                {
                    _dbContext.Coupons.Remove(obj);
                    _dbContext.SaveChanges();
                }
                else
                {
                    _response.Result =null;
                    _response.IsSuccess = false;
                }
            }

            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;

            }

            return _response;
        }

    }
}
