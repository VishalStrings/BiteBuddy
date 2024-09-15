using BiteBuddy.Web.Models;
using BiteBuddy.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace BiteBuddy.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ProductDto>? list = new();

            ResponseDto? responseDto = await _productService.GetAllProductsAsync();
            if (responseDto != null && responseDto.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(responseDto.Result));
            }
            else
            {
                TempData["error"] = responseDto?.Message;
            }
            return View(list); //Test
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto response = await _productService.CreateProductAsync(productDto);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Product Created Successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = response.Message;
                }
            }
            return View(productDto); //Test
        }

        public async Task<IActionResult> ProductDelete(int productId)
        {
            ResponseDto responseDto = await _productService.GetProductByIdAsync(productId);
            if (responseDto != null && responseDto.IsSuccess)
            {
                ProductDto? productModel = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(responseDto.Result));
                //return RedirectToAction(nameof(Index));
                return View(productModel);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductDto productDto)
        {
            ResponseDto responseDto = await _productService.DeleteProductByIdAsync(productDto.ProductId);
            if (responseDto != null && responseDto.IsSuccess)
            {
                TempData["success"] = "Product deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = responseDto?.Message;
            }
            return View(productDto);
        }

        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductDto productDto)
        {
            ResponseDto responseDto = await _productService.UpdateProductAsync(productDto);
            if (responseDto != null && responseDto.IsSuccess)
            {
                TempData["success"] = "Product updated successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = responseDto?.Message;
            }
            return View(productDto);
        }

    }
}