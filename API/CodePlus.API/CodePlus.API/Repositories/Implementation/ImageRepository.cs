using CodePlus.API.Data;
using CodePlus.API.Models.Domain;
using CodePlus.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodePlus.API.Repositories.Implementation
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ApplicationDbContext _context;

        public ImageRepository(IWebHostEnvironment environment, IHttpContextAccessor contextAccessor,
                               ApplicationDbContext context)
        {
            _environment = environment;
            _contextAccessor = contextAccessor;
            _context = context;
        }

       
        public async Task<BlogImage> Upload(IFormFile file, BlogImage blogImage)
        {
            // 1- Upload the Image to API/Images
            var localPath = Path.Combine(_environment.ContentRootPath, "Images", $"{blogImage.FileName}{blogImage.FileExtension}");
            using var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);

            // 2- Update the Database
            var httpRequest = _contextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{blogImage.FileName}{blogImage.FileExtension}";
            blogImage.Url = urlPath;
            await _context.BlogImages.AddAsync(blogImage);
            await _context.SaveChangesAsync();
            return blogImage;
        }

        public async Task<IEnumerable<BlogImage>> GetAll()
        {
            return await _context.BlogImages.ToListAsync();
        }


    }
}
