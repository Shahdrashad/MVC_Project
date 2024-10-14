using Demo.BLL.Interfaces;
using Demo.DAl.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
	[Authorize]
	public class DepartmentController : Controller
	{
		private readonly IDepartmentRepository _departmentRepository;
		public DepartmentController(IDepartmentRepository departmentRepository) // ask clr for creating object from class implement interface
		{
			_departmentRepository= departmentRepository;
		}

        //baseUrl /Dept/index
        #region Index
        public IActionResult Index()
		{
			var department=_departmentRepository.GetAll();
			return View(department);
		}
        #endregion

        #region Create

        [HttpGet]
		public IActionResult Create()
		{
			return View();
		
		}
		[HttpPost]
        public IActionResult Create(Department department)
        {
			if(ModelState.IsValid)
			{
				_departmentRepository.Add(department);
				return RedirectToAction(nameof(Index));
			}
            return View(department);

        }

		#endregion

		#region Details
		public IActionResult Details(int? id) => DepartmentControllerHandler(id, nameof(Details));

		#endregion

		#region Edit
		[HttpGet]
		public IActionResult Edit(int? id) => DepartmentControllerHandler(id, nameof(Edit));
		
		[HttpPost]
		public IActionResult Edit(Department department, [FromRoute] int id)
		{
			if (ModelState.IsValid)
			{
				try
				{
                    _departmentRepository.Update(department);
                    return RedirectToAction(nameof(Index));
                }
				catch(System.Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}
			}
			return View(department);

		}
        #endregion

         #region delete
		public IActionResult Delete(int? id)=>DepartmentControllerHandler(id,nameof(Delete));

        
		[HttpPost]
		
		public IActionResult Delete(Department department, [FromRoute] int id)
		{
			if((id!=department.Id))
			{
				return BadRequest();
			}
			try
			{
				_departmentRepository.Delete(department);
			}
			catch(System.Exception ex)
			{
				ModelState.AddModelError(string.Empty,ex.Message);
			}
			return View(department);
		}

        #endregion
        
        private IActionResult DepartmentControllerHandler(int? id,string viewname)
		{
            if (id == null) return BadRequest();// 400
            var department = _departmentRepository.GetById(id.Value);
            if (department == null)  return NotFound();
           
            return View(viewname,department);
        }
    }
}
