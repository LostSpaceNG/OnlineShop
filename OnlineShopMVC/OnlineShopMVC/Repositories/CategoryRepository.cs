using Microsoft.EntityFrameworkCore;
using OnlineShopMVC.Data;
using OnlineShopMVC.Models;
using OnlineShopMVC.Repositories.Interfaces;

namespace OnlineShopMVC.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            return (await _context.Categories.FirstOrDefaultAsync(c => c.Name == name))!;
        }
    }
}
