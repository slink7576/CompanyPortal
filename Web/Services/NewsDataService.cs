using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Services
{
    public class NewsDataService
    {
        INewsRepository _repositoryNews;
        IAsyncRepository<ChartItem> _repositoryChart;
        IEmployeeRepository _employeeRepository;
        INewsService _newsService;

        public NewsDataService(INewsRepository repositoryNews, IAsyncRepository<ChartItem> repositoryChart,
            INewsService newsService, IEmployeeRepository employeeRepository)
        {
            _repositoryNews = repositoryNews;
            _repositoryChart = repositoryChart;
            _newsService = newsService;
            _employeeRepository = employeeRepository;
        }
        public List<NewsItem> ListByPage(int pageSize, int page)
        {
            return _repositoryNews.ListAllQuery().OrderByDescending(c => c.Created)
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();
        }
    
        public async Task<List<NewsItem>> ListAllAsync()
        {
            return await _repositoryNews.ListAllAsync();
        }

        public int GetAllCount()
        {
            return _repositoryNews.ListAll().Count();
        }

        public async Task AddAsync(NewsItem news)
        {
            await _repositoryNews.AddAsync(news);
        }

        public async Task DeleteNewsItemAsync(int id)
        {
            await _newsService.DeleteNewsItem(id);
        }

        public async Task<NewsItem> GetByIdAsync(int id)
        {
            return await _repositoryNews.GetByIdAsync(id);
        }

        public async Task UpdateNewsItemAsync(NewsItem news, int id)
        {
            await _newsService.UpdateNewsItem(id, news.Header, news.Body, news.Author, news.PhotoURL);
        }

    }
}
