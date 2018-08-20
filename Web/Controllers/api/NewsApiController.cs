using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.api
{
    [Produces("application/json")]
    [Route("[controller]/[action]")]
    public class NewsController : Controller
    {
        IEmployeeRepository _employeeRepository;
        INewsService _newsService;
        UserManager<ApplicationUser> _userManager;

        public NewsController(INewsService newsService, UserManager<ApplicationUser> userManager, IEmployeeRepository employeeRepository)
        {
            _userManager = userManager;
            _newsService = newsService;
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<object> GetChart(int id)
        {
            return await _newsService.GetNewsChart(id);
        }

        [HttpPost]
        [Authorize]
        public async Task<object> Like(int id)
        {
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var employer = await _employeeRepository.GetByIdWithItemsAsync(appUser.EmployerId);
            try
            {
                await _newsService.Like(id, employer);
            }
            catch (Exception)
            {
                return BadRequest("You already liked this post!");
            }
            return Ok();
        }
    }
}