using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.Interfaces;
using Demo.DAl.Contexts;
using Demo.DAl.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MvcDbContext _dbContext;

        public GenericRepository(MvcDbContext dbContext) //_dbContext هذا يعني أنه كلما احتجت للتعامل مع قاعدة البيانات، سيتم استخدام
        {
            _dbContext = dbContext;

        }
        public int Add(T item)
        {
            _dbContext.Add(item);
            return _dbContext.SaveChanges();
        }

        public int Delete(T item)
        {
            _dbContext.Remove(item);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<T> GetAll() 
        { 
           if(typeof(T)==typeof(Employee))
             {
                return (IEnumerable<T>) _dbContext.Employees.Include(E=>E.Departments).ToList();
            }
          return  _dbContext.Set<T>().ToList();
        }


        public T GetById(int id) => _dbContext.Set<T>().Find(id);

       

        public int Update(T item)
        {
            _dbContext.Update(item);
            return _dbContext.SaveChanges();
        }
    }
}
