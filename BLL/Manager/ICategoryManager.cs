using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.BLL.Dtos.CategoryDto;
using LMS.BLL.Dtos.CourseDtos;
using LMS.DAL.Data.Models;

namespace LMS.BLL.Manager
{
    public interface ICategoryManager
    {
        CategoryReadDto GetByName(string name);
        void Insert(CategoryAddDto category);
        IEnumerable<CategoryReadDto> GetAll();
        
        void Delete(int Id);
        CategoryReadDto GetById(int id);
        void SaveChanges();
    }
}
