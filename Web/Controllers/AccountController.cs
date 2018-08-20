using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
   
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeRepository _employeeRepository;

        public int PageSize = 6;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IEmployeeService employeeService, IEmployeeRepository employeeRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _employeeService = employeeService;
            _employeeRepository = employeeRepository;
        }

        #region Login/Register Actions

        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "News");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Wrond login or password");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "News");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { Email = model.Email, UserName = model.Email, PhoneNumber = model.Phone};
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var employee = new Employee { Name = model.Name, PhotoURL = model.PhotoURL, Position = model.Position, Surname = model.Surname, Email = model.Email, PhoneNumber = model.Phone };
                    await _employeeRepository.AddAsync(employee);
                    employee = await _employeeRepository.GetByEmailAsync(employee.Email);
                    user.EmployerId = employee.Id;
                    await _userManager.UpdateAsync(user);
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "News");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        #endregion

        public IActionResult AccessDenied(string errorMessge)
        {
            return View(errorMessge);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            var user = await _userManager.FindByEmailAsync(employee.Email);
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                await _employeeRepository.DeleteAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> Index(string position = "", int page = 1, string name = "", bool desc = false)
        {
            if (name != "")
            {
                page = 1;
            }   
            var employers = await _employeeRepository.ListAllAsync();
            var sortedEmployers = employers
                .Where(p => (p.Position.ToString() == position || position == "") &&
                    (p.Name.ToLower().Contains(name.ToLower()) || p.Surname.ToLower().Contains(name.ToLower())))
                .OrderBy(p =>
                {
                    if (desc)
                    {
                        return p.Name[0];
                    }
                    else
                    {
                        return p.Id;
                    }

                });

            var view = new EmployeeListViewModel
            {
                Employers = sortedEmployers
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {

                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = sortedEmployers.Count()
                },
                CurrentName = name,
                IsDesc = desc


            };

            if (position != "")
                try
                {
                    view.CurrentPosition = (Position)Enum.Parse(typeof(Position), position);
                }
                catch (Exception)
                {

                }

            ViewBag.ReturnUrl = Request.Path;
            return View(view);
        }

        [Authorize]
        public async Task<IActionResult> EditMyself()
        {
            var userResult = await _userManager.FindByNameAsync(User.Identity.Name);
            return RedirectToAction("Edit", new { id = userResult.EmployerId });
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var currentEmployee = await _employeeRepository.GetByIdAsync(currentUser.EmployerId);
            if (!_userManager.GetUsersInRoleAsync("Admin").Result.Contains(currentUser))
            {
                if (currentEmployee.Id != currentUser.EmployerId)
                {
                    return View("AccessDenied", "Only owner or admin can edit user profile");
                }
            }
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (currentUser != null && employee != null)
            {
                return View(new RegisterViewModel
                {
                    Email = employee.Email,
                    Name = employee.Name,
                    Surname = employee.Surname,
                    Phone = employee.PhoneNumber,
                    PhotoURL = employee.PhotoURL,
                    Position = employee.Position,
                    
                });
            }
            else
            {
                return View("AccessDenied", "Not found such user");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RegisterViewModel model)
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!_userManager.GetUsersInRoleAsync("Admin").Result.Contains(currentUser))
            {
                if (model.Email != User.Identity.Name)
                {
                    return View("AccessDenied", "Only owner or admin can edit user profile");
                }
            }
            var employee = await _employeeRepository.GetByEmailAsync(model.Email);
            var updateuser = new Employee { Name = model.Name, Surname = model.Surname, PhotoURL = model.PhotoURL, Position = model.Position, Email = model.Email, PhoneNumber = model.Phone };
            await _employeeService.Update(employee.Id, updateuser);
            return RedirectToAction("Index", "News");
        }

        public async Task<IActionResult> Info(int id, string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            var result = await _employeeRepository.GetByIdAsync(id);
            if (result != null)
            {
                return View("AccountSummaryFull", result);
            }
            else
            {
                return View("AccessDenied", "Error");
            }
        }

        public IActionResult Users()
        {
            return View(_userManager.Users);
        }


    }
}