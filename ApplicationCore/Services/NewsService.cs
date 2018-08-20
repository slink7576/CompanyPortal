using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _NewsRepository;
        private readonly IAsyncRepository<ChartItem> _repositoryChart;
        public NewsService(INewsRepository NewsRepository, IAsyncRepository<ChartItem> repositoryChart)
        {
            _NewsRepository = NewsRepository;
            _repositoryChart = repositoryChart;
        }

        public async Task CreateNewsItem(NewsItem entity)
        {
            await _NewsRepository.AddAsync(entity);
        }

        public async Task DeleteNewsItem(int newsItemId)
        {
           
            var newsItem = await _NewsRepository.GetByIdWithItemsAsync(newsItemId);

            if (newsItem == null)
                throw new ArgumentNullException();

            if(newsItem != null && newsItem.ChartItems != null)
            {
                foreach (var chart in newsItem.ChartItems.AsEnumerable().ToList())
                {
                    await _repositoryChart.DeleteAsync(chart);
                }

            }

            await _NewsRepository.DeleteAsync(newsItem);

        }

        public async Task<IEnumerable<ChartItem>> GetNewsChart(int newsItemId)
        {
            var newsItem = await _NewsRepository.GetByIdWithItemsAsync(newsItemId);
            if (newsItem == null)
                throw new ArgumentNullException();
            return newsItem.ChartItems;

        }

        public async Task Like(int newsItemId, Employee sender)
        {
            var newsItem =  await _NewsRepository.GetByIdWithItemsAsync(newsItemId);
            if (newsItem == null)
                throw new ArgumentNullException();
            if (newsItem.Employees.Select(c => c.Id).Contains(sender.Id))
            {
                throw new AlreadyLikedException(sender.Id);
            }
            else
            {
                var chartForToday = newsItem.ChartItems.ToList().Find(c =>
                c.Date.Day == DateTime.Now.Day
                    && c.Date.Month == DateTime.Now.Month
                        && c.Date.Year == DateTime.Now.Year
                );
                if (chartForToday != null)
                {
                    chartForToday.Likes++;
                    newsItem.TotalLikes++;
                    newsItem.Employees.Add(sender);
                    await _NewsRepository.UpdateAsync(newsItem);
                }
                else
                {
                    var chartItem = new ChartItem { Date = DateTime.Now, Likes = 1};
                    newsItem.TotalLikes++;
                    newsItem.Employees.Add(sender);
                    newsItem.ChartItems.Add(chartItem);
                    await _NewsRepository.UpdateAsync(newsItem);
                }
            }
        }

        public async Task UpdateNewsItem(int newsItemId, string header, string body, string author, string photoUrl)
        {
            var itemToUpdate = await _NewsRepository.GetByIdAsync(newsItemId);
            if (itemToUpdate == null)
                throw new ArgumentNullException();
            itemToUpdate.Header = header;
            itemToUpdate.Body = body;
            itemToUpdate.Author = author;
            itemToUpdate.PhotoURL = photoUrl;
            await _NewsRepository.UpdateAsync(itemToUpdate);
        }
    }
}