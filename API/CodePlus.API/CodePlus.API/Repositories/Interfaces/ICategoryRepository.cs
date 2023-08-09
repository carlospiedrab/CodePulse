using CodePlus.API.Models.Domain;

namespace CodePlus.API.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);

        Task<IEnumerable<Category>> GetAllAsync();

        Task<Category> GetById(Guid id);
    }
}
