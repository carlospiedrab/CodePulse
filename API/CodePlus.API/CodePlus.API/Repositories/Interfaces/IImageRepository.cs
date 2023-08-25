using CodePlus.API.Models.Domain;

namespace CodePlus.API.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<BlogImage> Upload(IFormFile file, BlogImage blogImage);
        Task<IEnumerable<BlogImage>> GetAll();
    }
}
