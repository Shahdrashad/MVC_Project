﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.Interfaces;
using Demo.DAl.Contexts;
using Demo.DAl.Models;

namespace Demo.BLL.Repositories
{
	public class DepartmentRepository : GenericRepository<Department>,IDepartmentRepository
	{
        #region

        //private  readonly MvcDbContext _dbContext;
        //public DepartmentRepository(MvcDbContext dbContext) // Ask clr for obj from DbContext
        //{
        //	_dbContext = dbContext;

        //}

        //public int Add(Department department)
        //{
        //	_dbContext.Add(department);
        //	return _dbContext.SaveChanges();

        //}

        //public int  Delete(Department department)
        //{
        //	_dbContext.Remove(department);
        //	return _dbContext.SaveChanges();
        //}

        //public IEnumerable<Department> GetAll() => _dbContext.Departments.ToList();




        //public Department GetById(int id)
        //{
        //	return _dbContext.Departments.Find(id);

        //}

        //public int Update(Department department)
        //{
        //	_dbContext.Update(department);
        //	return _dbContext.SaveChanges();
        //}
        #endregion
       
        public DepartmentRepository(MvcDbContext context) :base(context)
        {
        }

        
    }
}