using OnlineShopMVC.Models;

namespace OnlineShopMVC.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);

        // For advanced filtering
        IQueryable<Product> GetAllQueryable();
    }
}
