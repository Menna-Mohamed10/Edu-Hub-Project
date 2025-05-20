using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DAL.Data;
using LMS.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.DAL.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly LMSContext _context; 

        public CategoryRepository(LMSContext context)
        {
            _context = context;
        }

        public Category GetByName(string name)
        {
            return _context.Categories.FirstOrDefault(a => a.Name.ToLower().Contains(name.ToLower()));
        }

       
        public IQueryable<Category> GetAll()
        {
            return _context.Categories.Include(c => c.Courses).AsNoTracking();
        }
        

        public void Insert(Category category)
        {
            _context.Categories.Add(category);
        }

        public void Delete(Category category)
        {
            _context.Remove(category);
        }

        public void Update(Category category)
        {

        }
        public Category GetById(int id)
        {
            return _context.Categories
    .Include(c => c.Courses)
    .SingleOrDefault(c => c.Id == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
