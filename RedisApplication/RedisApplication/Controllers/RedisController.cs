using Microsoft.AspNetCore.Mvc;
using RedisApplication.Interfaces;
using RedisApplication.Models;

namespace RedisApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RedisController : ControllerBase
    {
        private readonly IProductService _productService;

        private readonly IRedisCacheService _redisCacheService;

        public RedisController(IProductService productService, IRedisCacheService redisCacheService)
        {
            _productService = productService;
            _redisCacheService = redisCacheService;
        }

        [HttpGet(Name = nameof(GetAllProducts))]
        public async Task<IActionResult> GetAllProducts()
        {
            const string cacheKey = "all_products";

            var cacheProducts = _redisCacheService.GetData<IEnumerable<Product>>(cacheKey);

            if (cacheProducts is not null)
            {
                return Ok(cacheProducts);
            }

            cacheProducts = await _productService.GetAllProductsAsync();

            _redisCacheService.SetData(cacheKey, cacheProducts);

            return Ok(cacheProducts);
        }
    }
}
