using BiteBuddy.Web.Models;
using BiteBuddy.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BiteBuddy.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartbasedonLoggedInUser());
        }

        public async Task<CartDto> LoadCartbasedonLoggedInUser()
        {
            //is used to extract a specific claim value from the user's claims in an ASP.NET Core 
            //    application, typically in the context of handling JWT (JSON Web Token) 
            //    authentication
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)
                ?.FirstOrDefault()?.Value;
            ResponseDto? response = await _cartService.GetCartByUserIdAsync(userId);
            if (response != null && response.IsSuccess)
            {
                CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result));
                return cartDto;
            }

            return new CartDto();
        }

        public async Task<IActionResult> RemoveCoupon(int cartDetailId)
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)
                 ?.FirstOrDefault()?.Value;
            ResponseDto? response = await _cartService.RemoveCartAsync(userId);
            if (response != null && response.IsSuccess)
            {
                CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result));
                return cartDto;
            }

            return new CartDto();
        }
    }
}
