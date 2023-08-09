using CodePlus.API.Data;
using CodePlus.API.Models.Domain;
using CodePlus.API.Models.Dto;
using CodePlus.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePlus.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };

           await _categoryRepository.CreateAsync(category);

            var response = new CategoryDto { Id =category.Id,  Name = category.Name, UrlHandle = category.UrlHandle };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
           return Ok(categories.Select(c => new CategoryDto { Id=c.Id, Name=c.Name,UrlHandle=c.UrlHandle }));                            
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetAllCategoryById(Guid id)
        {
            var existingCategory = await _categoryRepository.GetById(id);
            if(existingCategory is null)
            {
                return NotFound();
            }
            var response = new CategoryDto
            {
                Id = existingCategory.Id,
                Name = existingCategory.Name,
                UrlHandle = existingCategory.UrlHandle
            };
            return Ok(response);
        }

    }
}
