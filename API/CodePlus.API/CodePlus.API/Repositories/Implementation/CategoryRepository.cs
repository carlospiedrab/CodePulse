﻿using CodePlus.API.Data;
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

        public async Task<Category> UpdateAsync(Category category)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
            if(existingCategory != null)
            {
                _context.Entry(existingCategory).CurrentValues.SetValues(category);
                await _context.SaveChangesAsync();
                return category;
            }
            return null;
        }

        public async Task<Category> DeleteAsync(Guid id)
        {
            var existeingCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
         
                _context.Categories.Remove(existeingCategory);
                await _context.SaveChangesAsync();
                return existeingCategory;
            
        }
    }
}
