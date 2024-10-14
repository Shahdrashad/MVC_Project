using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.Repositories;
using Demo.DAl.Contexts;
using Demo.DAl.Models;

namespace Demo.BLL.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
        #region
        //IEnumerable<Employee> GetAll();
        //Employee  GetById(int id);
        //int Add(Employee employee);
        //int Update(Employee employee);
        //int Delete(Employee employee);
        #endregion

       
        IQueryable<Employee> GetEmployeesByAddress(string address);
        IQueryable<Employee> GetEmployeesByName(string Search);
    }
}
