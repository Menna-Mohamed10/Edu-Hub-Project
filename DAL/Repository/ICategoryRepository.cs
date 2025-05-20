using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DAL.Data.Models;

namespace LMS.DAL.Repository
{
    
        public interface ICategoryRepository
        {
            Category GetByName(string name); 
            IQueryable<Category> GetAll();
        void Delete(Category category);
        void Update(Category category);
        Category GetById(int id);
            void Insert(Category category);
            void SaveChanges();
        }
    
}
