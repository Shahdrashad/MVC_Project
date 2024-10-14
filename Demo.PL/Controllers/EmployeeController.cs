using System.Collections;
using System.Collections.Generic;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAl.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IEmployeeRepository employeeRepository,IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        #region Index
        public IActionResult Index(string Search)
        {
            IEnumerable<Employee> employees;

           if ( string.IsNullOrEmpty(Search))
            {
                 employees = _employeeRepository.GetAll();
               
            }
           else
            {
                 employees =_employeeRepository.GetEmployeesByName(Search);
            }
            return View(employees);

        }
        #endregion
      
        #region Create

       
        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(_departmentRepository.GetAll(), "Id", "Name");
            return View();


        }
        [HttpGet]
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
        
            if (ModelState.IsValid)
            {
                _employeeRepository.Add(employee);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments = new SelectList(_departmentRepository.GetAll(), "Id", "Name");
           
            return View(employee);

        }

        #endregion
        #region Details
        public IActionResult Details(int? id) => EmployeeControllerHandler(id, nameof(Details));

        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.Departments = new SelectList(_departmentRepository.GetAll(), "Id", "Name");
            return EmployeeControllerHandler(id, nameof(Edit));
        }
            

        [HttpPost]
        public IActionResult Edit(Employee employee, [FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _employeeRepository.Update(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            ViewBag.Departments = new SelectList(_departmentRepository.GetAll(), "Id", "Name");
            return View(employee);

        }
        #endregion

        #region delete
        public IActionResult Delete(int? id) => EmployeeControllerHandler(id, nameof(Delete));


        [HttpPost]

        public IActionResult Delete(Employee employee, [FromRoute] int id)
        {
            if ((id != employee.Id))
            {
                return BadRequest();
            }
            try
            {
                _employeeRepository.Delete(employee);
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(employee);
        }

        #endregion

        private IActionResult EmployeeControllerHandler(int? id, string viewname)
        {
            if (id == null) return BadRequest();// 400
            var employee = _employeeRepository.GetById(id.Value);
            if (employee == null) return NotFound();

            return View(viewname, employee);
        }


    }
}
