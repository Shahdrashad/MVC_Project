using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Demo.DAl.Models;
using Demo.PL.Helper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public UserController(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}
		public async Task<IActionResult> Index(string SearchValue)
		{

			
                // إرجاع جميع المستخدمين في حالة عدم وجود قيمة بحث
                var Users = string.IsNullOrEmpty(SearchValue)
				?	await _userManager.Users.Select(
					U => new UserViewModel()
					{
						Id = U.Id,
						FName = U.FirstName,
						LName = U.LastName,
						UserName = U.UserName,
						Email = U.Email,
						Role = _userManager.GetRolesAsync(U).GetAwaiter().GetResult()



					}).ToListAsync()
			       : await _userManager.Users      // البحث في الاسم الأول أو الاسم الأخير أو اسم المستخدم أو البريد الإلكتروني
                .Where(u => u.FirstName.Contains(SearchValue)
				|| u.LastName.Contains(SearchValue) ||
                    u.UserName.Contains(SearchValue) ||
                    u.Email.Contains(SearchValue))
				.Select(u => new UserViewModel()
				{
					Id = u.Id,
					FName = u.FirstName,
					LName = u.LastName,
					UserName = u.UserName,
					Email = u.Email,
					Role = _userManager.GetRolesAsync(u).GetAwaiter().GetResult()

				}).ToListAsync();

           

            return View(Users);




		}

		public async Task<IActionResult> Details( string id,string viewName=nameof(Details))
		{
			if (string.IsNullOrWhiteSpace(id)) return BadRequest();
			var user =await _userManager.FindByIdAsync (id);
			if (user is null) return NotFound();
			var model = new UserViewModel()
			{

				Id = user.Id,
				FName = user.FirstName,
				LName = user.LastName,
				UserName = user.UserName,
				Email = user.Email,
				Role = await _userManager.GetRolesAsync(user)


			};
			return View( viewName, model);


        }

		public async Task<IActionResult> Edit (string id)=>await Details (id,nameof(Edit));
		[HttpPost]
        public async Task<IActionResult> Edit(string id,UserViewModel model)
		{
            if (id != model.Id) return BadRequest();
			if (ModelState.IsValid) 
			{

				try
				{
					var user = await _userManager.FindByEmailAsync(model.Email);
					if (user is null) return NotFound();
					user.FirstName = model.FName;
					user.LastName = model.LName;
					await _userManager.UpdateAsync(user);

					return RedirectToAction(nameof(Index));

				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}
			}
			return View(model);

        }

        public async Task<IActionResult> Delete(string id) =>await Details(id, nameof(Delete));
		[ActionName ("Delete")]
		[HttpPost]

        public async Task<IActionResult> ConfirmDelete(string id)
		{
			
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user is null) return NotFound();
              
                await _userManager.DeleteAsync(user);
				return RedirectToAction(nameof(Index));

               
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View();

        }
    }
}

