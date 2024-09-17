using AutoMapper;
using BiteBuddy.Services.ShoppingCartAPI.Data;
using BiteBuddy.Services.ShoppingCartAPI.Models;
using BiteBuddy.Services.ShoppingCartAPI.Models.Dto;
using BiteBuddy.Services.ShoppingCartAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.PortableExecutable;

namespace BiteBuddy.Services.ShoppingCartAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class ShoppingCartAPIController : ControllerBase
    {
        private ResponseDto _response;
        private IMapper _mapper;
        private readonly ApplicationDbContext _db;
        private IProductService _productService;
        private ICouponService _couponService;

        public ShoppingCartAPIController(ApplicationDbContext db,
            IMapper mapper, IProductService productService, ICouponService couponService)
        {
            _db = db;
            _productService = productService;
            this._response = new ResponseDto();
            _mapper = mapper;
            _couponService = couponService;
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<ResponseDto> GetCart(string userId)
        {
            try
            {
                CartDto cart = new CartDto();
                var carheader = _db.CartHeaders?.FirstOrDefault(u => u.UserId == userId);
                cart.CartHeader = _mapper.Map<CartHeaderDto>(carheader);


                cart.CartDetails = _mapper.Map<IEnumerable<CartDetailsDto>>(_db.CartDetails
                    .Where(u => u.CartHeaderId == cart.CartHeader.CartHeaderId));

                IEnumerable<ProductDto> productDtos = await _productService.GetProducts(); // Load all product

                foreach (var item in cart.CartDetails)
                {
                    item.Product = productDtos.FirstOrDefault(u => u.ProductId == item.ProductId);
                    cart.CartHeader.CartTotal += ((double)(item.Count * item.Product.Price));
                }

                //apply coupon if any
                if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
                {
                    CouponDto coupon = await _couponService.GetCoupon(cart.CartHeader.CouponCode);
                    if (coupon != null && cart.CartHeader.CartTotal > (double)coupon.MinimumAmount)
                    {
                        cart.CartHeader.CartTotal -= (double)coupon.DiscountAmount;
                        cart.CartHeader.Discount = (double)coupon.DiscountAmount;
                    }
                }

                _response.Result = cart;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("CartUpsert")]
        public async Task<ResponseDto> CartUpsert(CartDto cartDto)
        {
            try
            {
                var cartHeaderFromDb = _db.CartHeaders.AsNoTracking().FirstOrDefault(u => u.UserId == cartDto.CartHeader.UserId);
                if (cartHeaderFromDb == null)
                {
                    // if null then create card headers and details
                    CartHeader cartHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);
                    _db.CartHeaders.Add(cartHeader);
                    await _db.SaveChangesAsync();

                    cartDto.CartDetails.First().CartHeaderId = cartHeader.CartHeaderId;
                    _db.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                    await _db.SaveChangesAsync();
                }
                else
                {
                    //asnoTracking use so that Ef shold not get confused.
                    // if header is not Null check if details have same product or not
                    var cardDetailsFromDb = await _db.CartDetails.AsNoTracking().FirstOrDefaultAsync
                               (u => u.ProductId == cartDto.CartDetails.First().ProductId &&
                               u.CartHeaderId == cartHeaderFromDb.CartHeaderId);
                    if (cardDetailsFromDb == null)
                    {
                        // Create CartDetails
                        cartDto.CartDetails.First().CartHeaderId = cardDetailsFromDb.CartHeaderId;
                        _db.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        await _db.SaveChangesAsync();
                    }
                    else
                    {
                        //update count in Cart
                        cartDto.CartDetails.First().Count += cardDetailsFromDb.Count;
                        cartDto.CartDetails.First().CartDetailsId = cardDetailsFromDb.CartHeaderId;
                        cartDto.CartDetails.First().CartDetailsId = cardDetailsFromDb.CartDetailsId;
                        _db.CartDetails.Update(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        await _db.SaveChangesAsync();
                    }
                }
                _response.Result = cartDto;
            }

            catch (Exception ex)
            {
                _response.Message = ex.InnerException.ToString();
                _response.IsSuccess = false;

            }
            return _response;

        }


        [HttpPost("RemoveCart")]
        public async Task<ResponseDto> RemoveCart([FromBody] int cartDetailsId)
        {
            try
            {

                CartDetails cartDetails = _db.CartDetails
                    .First(u => u.CartDetailsId == cartDetailsId);

                int totalCountofcartItems = _db.CartDetails.Where(u => u.CartHeaderId == cartDetailsId).Count();

                _db.CartDetails.Remove(cartDetails);
                if (totalCountofcartItems == 1)
                {
                    var cartHeaderToRemove = await _db.CartHeaders
                        .FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CartHeaderId);

                    _db.CartHeaders.Remove(cartHeaderToRemove);
                }

                await _db.SaveChangesAsync();

                _response.Result = true;
            }

            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;

            }
            return _response;

        }

        [HttpPost("ApplyCoupon")]
        public async Task<object> ApplyCoupon([FromBody] CartDto cartDto)
        {
            try
            {
                var cartFromDb = await _db.CartHeaders.FirstAsync(u => u.UserId == cartDto.CartHeader.UserId);
                cartFromDb.CouponCode = cartDto.CartHeader.CouponCode;
                _db.CartHeaders.Update(cartFromDb);
                await _db.SaveChangesAsync();
                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.ToString();
            }
            return _response;
        }
    }
}
