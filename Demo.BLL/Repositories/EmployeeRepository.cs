using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.Interfaces;
using Demo.DAl.Contexts;
using Demo.DAl.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly MvcDbContext _context;

        public EmployeeRepository(MvcDbContext context) : base(context)
        {
            _context = context;
        }
        public IQueryable<Employee> GetEmployeesByAddress(string address)
        {
            return _context.Employees.Where(e => e.Address == address).Include(e=>e.Departments);
        }

        public IQueryable<Employee> GetEmployeesByName(string Search)
        {
            return _context.Employees
                .Include(e=>e.Departments)
                .Where(_e => _e.Name.ToLower().Contains(Search.ToLower()));

        }
    }
}
