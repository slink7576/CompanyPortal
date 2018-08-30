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
using Web.Services;
using Web.ViewModels;

namespace Web.Controllers
{
    public class NewsController : Controller
    {
        NewsDataService _newsViewModelService;
        UserManager<ApplicationUser> _userManager;
        public int PageSize = 4;

        public NewsController(UserManager<ApplicationUser> userManager, NewsDataService newsViewModelService)
        {
            _userManager = userManager;
            _newsViewModelService = newsViewModelService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var view = new NewsItemListViewModel
            {
                News = _newsViewModelService.ListByPage(PageSize, page),
                PagingInfo = new PagingInfo
                {

                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _newsViewModelService.GetAllCount()
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
                await _newsViewModelService.DeleteNewsItemAsync(id);
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
                return View(await _newsViewModelService.GetByIdAsync((int)id));
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
                    await _newsViewModelService.UpdateNewsItemAsync(news, (int)id);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    await _newsViewModelService.AddAsync(news);
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
            return View(await _newsViewModelService.GetByIdAsync(id));
        }
    }
}