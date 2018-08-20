using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class NewsController : Controller
    {
        INewsRepository _repositoryNews;
        IAsyncRepository<ChartItem> _repositoryChart;
        IEmployeeRepository _employeeRepository;
        INewsService _newsService;
        UserManager<ApplicationUser> _userManager;
        public int PageSize = 4;

        public NewsController(INewsRepository repositoryNews, IAsyncRepository<ChartItem> repositoryChart,
            INewsService newsService, UserManager<ApplicationUser> userManager, IEmployeeRepository employeeRepository)
        {
            _repositoryNews = repositoryNews;
            _repositoryChart = repositoryChart;
            _userManager = userManager;
            _newsService = newsService;
            _employeeRepository = employeeRepository;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var news = await _repositoryNews.ListAllAsync();
            var view = new NewsItemListViewModel
            {
                News = news.OrderBy(c => c.Created).Reverse()
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {

                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = news.Count()
                },
            };
            ViewBag.ReturnUrl = Request.Path;
            return View(view);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _newsService.DeleteNewsItem(id);
            }
             catch(Exception)
            {
                return View("AccessDenied", "Error while deleting object");
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(int? id)
        {
            if (id != null)
            {
                return View(await _repositoryNews.GetByIdAsync((int)id));
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsItem news, int? id)
        {
            if (ModelState.IsValid)
            {
                news.Author = User.Identity.Name;
                if (id != null)
                {
                    await _newsService.UpdateNewsItem((int)id, news.Header, news.Body, news.Author, news.PhotoURL);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    await _repositoryNews.AddAsync(news);
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                return View();
            }

        }

        public async Task<IActionResult> Info(int id, string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View(await _repositoryNews.GetByIdAsync(id));
        }
    }
}