using RedisApplication.Interfaces;
using RedisApplication.Models;

namespace RedisApplication.Services
{
    public class ProducService : IProductService
    {
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await Task.FromResult(
                new List<Product>()
                {
                    new Product { Name = "Product 1", Price = 100.00m },
                    new Product { Name = "Product 2", Price = 200.00m },
                    new Product { Name = "Product 3", Price = 300.00m }
                }.AsEnumerable()
            );
        }
    }
}
