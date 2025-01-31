using OnlineShopMVC.Models;

namespace OnlineShopMVC.Repositories.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category> GetByNameAsync(string name);
    }
}
