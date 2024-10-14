using Demo.DAl.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace Demo.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Create() { return View(); }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
           if(!ModelState.IsValid) return View();
            var role = new IdentityRole
            {
                Name = model.Name,
            };
            var result=await _roleManager.CreateAsync(role);
            if(result.Succeeded) return RedirectToAction("Index");
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);



        }


        public async Task<IActionResult> Index(string name)
        {

            if (string.IsNullOrEmpty(name))
            {
                var Roles = await _roleManager.Roles.Select(
                    R => new RoleViewModel()
                    {
                        Id = R.Id, 
                        Name = R.Name,



                    }).ToListAsync();
                return View(Roles);

            }
            var role = await _roleManager.FindByNameAsync(name);
            if (role is null) return View(Enumerable.Empty<RoleViewModel>());
            var model = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name,
               
            };

            return View(model);




        }

        public async Task<IActionResult> Details(string id, string viewName = nameof(Details))
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null) return NotFound();
            var model = new RoleViewModel()
            {

                Id = role.Id,
               Name= role.Name,


            };
            return View(viewName, model);


        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id) => await Details(id, nameof(Edit));
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleViewModel model)
        {
            if (id != model.Id) return BadRequest();
            if (ModelState.IsValid) 
            {

                try
                {
                    var role = await _roleManager.FindByIdAsync(model.Id);
                    if (role is null) return NotFound();
                   role.Name = model.Name;
                    await _roleManager.UpdateAsync(role);

                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(model);

        }

        public async Task<IActionResult> Delete(string id) => await Details(id, nameof(Delete));
        [ActionName("Delete")]
        [HttpPost]

        public async Task<IActionResult> ConfirmDelete(string id)
        {

            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role is null) return NotFound();

                await _roleManager.DeleteAsync(role);
                return RedirectToAction(nameof(Index));


            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View();

        }
        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUser (string roleid)
        {
            var role =await _roleManager.FindByIdAsync(roleid);
            if (role is null) return NotFound();
            ViewBag.RoleId=roleid;
            var users =await _userManager.Users.ToListAsync();
            var usersInRole=new List<UserRoleViewModel>();
            foreach (var item in users)
            {
                var UserInRole = new UserRoleViewModel
                { 
                    UserId = item.Id,
                    UserName=item.UserName,
                    IsInRole=await _userManager.IsInRoleAsync(item,role.Name)

                };
                usersInRole.Add(UserInRole);

                
            }
            return View(usersInRole);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUser(string roleid, List<UserRoleViewModel> users)
        {
            var role = await _roleManager.FindByIdAsync(roleid);
            if (role is null) return NotFound();
            if (ModelState.IsValid)
            {
                foreach (var item in users)
                {
                    var appUser = await _userManager.FindByIdAsync(item.UserId);
                    if (appUser is null) return NotFound();
                    // Add role 
                    if (item.IsInRole && !await _userManager.IsInRoleAsync(appUser, role.Name)) 
                    await _userManager.AddToRoleAsync(appUser, role.Name);
                    
                    //remove role 
                    if (!item.IsInRole && await _userManager.IsInRoleAsync(appUser, role.Name)) 
                    await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                }
                return RedirectToAction(nameof(Edit),new {id=roleid});

            }
            return View(users);
        }
}
}
