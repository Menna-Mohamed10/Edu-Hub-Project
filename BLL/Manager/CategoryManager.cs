using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LMS.BLL.Dtos.CategoryDto;
using LMS.BLL.Dtos.CourseDtos;
using LMS.DAL.Data.Models;
using LMS.DAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace LMS.BLL.Manager
{
    public class CategoryManager : ICategoryManager
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryManager(ICategoryRepository categoryRepository, IMapper mapper) 
        {
            _categoryRepository = categoryRepository ;
            _mapper = mapper;
        }
        public CategoryReadDto GetByName(string name)
        {
            return _mapper.Map<CategoryReadDto>(_categoryRepository.GetByName(name));
        }

        public void Insert(CategoryAddDto category)
        {
            bool exists = _categoryRepository
        .GetAll()
        .Any(c => c.Name.ToLower() == category.Name.ToLower());

    if (exists)
    {
        throw new InvalidOperationException("A category with this name already exists.");
    }
            _categoryRepository.Insert(_mapper.Map<Category>(category));
            _categoryRepository.SaveChanges();
        }
        public IEnumerable<CategoryReadDto> GetAll()
        {
            return _mapper.Map<IEnumerable<CategoryReadDto>>(_categoryRepository.GetAll().ToList());
        }



        public void SaveChanges()
        {

            _categoryRepository.SaveChanges();  
        }

        public void Delete(int Id)
        {
            var category = _categoryRepository.GetById(Id);
            if (category != null)
            {
                _categoryRepository.Delete(category);
                _categoryRepository.SaveChanges();
            }
            else throw new Exception("empty category");
        }

        public CategoryReadDto GetById(int id)
        {
            return _mapper.Map<CategoryReadDto>(_categoryRepository.GetById(id));
        }
    }
}
