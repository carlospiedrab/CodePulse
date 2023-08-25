using Azure.Core;
using CodePlus.API.Models.Domain;
using CodePlus.API.Models.Dto;
using CodePlus.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePlus.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {

        private readonly IBlogPostRepository _blogPostRepository;
        private readonly ICategoryRepository _categoryRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
        {
            _blogPostRepository = blogPostRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        [Authorize(Roles ="Writer")]
        public async Task<IActionResult> CreateBlogPost(CreateBlogPostRequestDto request)
        {
            var blogPost = new BlogPost
            {
                Title = request.Title,
                Content = request.Content,
                Author = request.Author,
                FeatureImageUrl = request.FeatureImageUrl,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                ShortDescription = request.ShortDescription,
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>() 
            };

            foreach (var categoryGuid in request.Categories)
            {
                var existeingCategory = await _categoryRepository.GetById(categoryGuid);
                if(existeingCategory != null)
                {
                    blogPost.Categories.Add(existeingCategory);
                }
            }

            blogPost = await _blogPostRepository.CreateAsync(blogPost);
            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                PublishedDate = blogPost.PublishedDate,
                ShortDescription = blogPost.ShortDescription,
                UrlHandle = blogPost.UrlHandle,
                IsVisible=blogPost.IsVisible,
                FeatureImageUrl= blogPost.FeatureImageUrl,
                Title = blogPost.Title ,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id=x.Id,
                    Name=x.Name,
                    UrlHandle=x.UrlHandle
                }).ToList()
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
           var blogPosts =  await _blogPostRepository.GetAllAsync();

            var response = blogPosts.Select(x => new BlogPostDto
            { 
                Id = x.Id,
                Author = x.Author,
                Content = x.Content,
                PublishedDate = x.PublishedDate,
                ShortDescription = x.ShortDescription,
                FeatureImageUrl = x.FeatureImageUrl,
                Title = x.Title,
                UrlHandle = x.UrlHandle,
                IsVisible = x.IsVisible,
                Categories = x.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()

            });
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBlogPostById(Guid id)
        {
            var blogPost = await _blogPostRepository.GetByIdAsync(id);
            if (blogPost is null)
            {
                return NotFound();
            }
            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                PublishedDate = blogPost.PublishedDate,
                ShortDescription = blogPost.ShortDescription,
                UrlHandle = blogPost.UrlHandle,
                IsVisible = blogPost.IsVisible,
                FeatureImageUrl = blogPost.FeatureImageUrl,
                Title = blogPost.Title,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("{urlHandle}")]
        public async Task<IActionResult> GetBlogPostByUrlHandle(string urlHandle)
        {
            var blogPost = await _blogPostRepository.GetByUrlHandleAsync(urlHandle);
            if (blogPost is null)
            {
                return NotFound();
            }
            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                PublishedDate = blogPost.PublishedDate,
                ShortDescription = blogPost.ShortDescription,
                UrlHandle = blogPost.UrlHandle,
                IsVisible = blogPost.IsVisible,
                FeatureImageUrl = blogPost.FeatureImageUrl,
                Title = blogPost.Title,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };
            return Ok(response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateBlogPostById(Guid id, UpdateBlogPostRequestDto request)
        {
            var blogPost = new BlogPost
            {
                Id = id,
                Title = request.Title,
                Content = request.Content,
                Author = request.Author,
                FeatureImageUrl = request.FeatureImageUrl,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                ShortDescription = request.ShortDescription,
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>()
            };

            foreach (var categoryGuid in request.Categories)
            {
                var existeingCategory = await _categoryRepository.GetById(categoryGuid);
                if (existeingCategory != null)
                {
                    blogPost.Categories.Add(existeingCategory);
                }
            }
            var updatedBlogPost = await _blogPostRepository.UpdateAsync(blogPost);
            if(updatedBlogPost == null)
            {
                return NotFound();
            }
            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                PublishedDate = blogPost.PublishedDate,
                ShortDescription = blogPost.ShortDescription,
                UrlHandle = blogPost.UrlHandle,
                IsVisible = blogPost.IsVisible,
                FeatureImageUrl = blogPost.FeatureImageUrl,
                Title = blogPost.Title,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteBlogPost(Guid id)
        {
            var deleteBlogPost = await _blogPostRepository.DeleteAsync(id);
            if(deleteBlogPost == null)
            {
                return NotFound();
            }

            var response = new BlogPostDto
            {
                Id = deleteBlogPost.Id,
                Author = deleteBlogPost.Author,
                Content = deleteBlogPost.Content,
                PublishedDate = deleteBlogPost.PublishedDate,
                ShortDescription = deleteBlogPost.ShortDescription,
                UrlHandle = deleteBlogPost.UrlHandle,
                IsVisible = deleteBlogPost.IsVisible,
                FeatureImageUrl = deleteBlogPost.FeatureImageUrl,
                Title = deleteBlogPost.Title
               
            };
            return Ok(response);

        }
    }
}
