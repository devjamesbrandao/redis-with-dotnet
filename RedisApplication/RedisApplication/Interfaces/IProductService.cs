using RedisApplication.Models;

namespace RedisApplication.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
    }
}
