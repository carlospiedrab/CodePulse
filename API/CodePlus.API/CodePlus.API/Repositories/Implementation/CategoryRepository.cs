using CodePlus.API.Data;
using CodePlus.API.Models.Domain;
using CodePlus.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodePlus.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetById(Guid id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c=>c.Id==id);
        }
    }
}
