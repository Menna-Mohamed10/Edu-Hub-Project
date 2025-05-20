using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS.DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly LMSContext _context;
        private readonly DbSet<T> _entities;

        public Repository(LMSContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public T GetById(int id)
        {
            return _entities.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.ToList();
        }

        public void Insert(T entity)
        {
            _entities.Add(entity);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        public void Delete(T entity)
        {
            _entities.Remove(entity);
        }

        public IQueryable<T> GetQueryable()
        {
            return _entities.AsQueryable();
        }
    }
}
