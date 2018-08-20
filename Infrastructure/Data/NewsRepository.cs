using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.IntermediateEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class NewsRepository : EfRepository<NewsItem>, INewsRepository
    {
      
        public NewsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }
        public override NewsItem Add(NewsItem newsItem)
        {
            _dbContext.News.Add(Mapper.MapToNewsItemDto(newsItem));
            _dbContext.SaveChanges();
            return newsItem;
        }

        public override async Task<NewsItem> AddAsync(NewsItem newsItem)
        {
            var mapper = Mapper.MapToNewsItemDto(newsItem);
            await _dbContext.News.AddAsync(Mapper.MapToNewsItemDto(newsItem));
            await _dbContext.SaveChangesAsync();
            return newsItem;
        }

        public override void Update(NewsItem newsItem)
        {
            var result = Mapper.MapToNewsItemDto(newsItem);
            var source = _dbContext.News
                .Include(c => c.ChartItems)
                .Include(c => c.EmployeeNewsItems)
               .Where(c => c.Id == newsItem.Id).FirstOrDefault();
            source.Author = result.Author;
            source.Body = result.Body;
            source.Created = result.Created;
            source.ChartItems = newsItem.ChartItems;
            source.TotalLikes = result.TotalLikes;
            source.EmployeeNewsItems = result.EmployeeNewsItems;
            _dbContext.SaveChanges();
        }

        public override async Task UpdateAsync(NewsItem newsItem)
        {
            var source = await _dbContext.News
                   .Include(c => c.ChartItems)
                   .Include(c => c.EmployeeNewsItems).ThenInclude(c => c.NewsItemDTO)
                  .Where(c => c.Id == newsItem.Id).FirstOrDefaultAsync();
            if(newsItem.Employees != null)
            {
                foreach (Employee e in newsItem.Employees)
                {
                    _dbContext.EmployeeNewsItems.Add(new EmployeeNewsItem { EmployeeId = e.Id, NewsItemId = newsItem.Id });
                }
            }
           
            var result = Mapper.MapToNewsItemDto(newsItem);
            
            source.Author = result.Author;
            source.Body = result.Body;
            source.Created = result.Created;
            source.TotalLikes = result.TotalLikes;
            source.ChartItems.Add(result.ChartItems.Last());

            _dbContext.Entry(source).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public override void Delete(NewsItem newsItem)
        {
            _dbContext.News.Remove(_dbContext.News.Find(newsItem.Id));
            _dbContext.SaveChanges();
        }

        public override async Task DeleteAsync(NewsItem newsItem)
        {
            _dbContext.News.Remove(await _dbContext.News.FindAsync(newsItem.Id));
            await _dbContext.SaveChangesAsync();
        }

        public NewsItem GetByIdWithItems(int id)
        {
            var newsItemDto = _dbContext.News
                .Include(c => c.ChartItems)
                .Include(c => c.EmployeeNewsItems)
                .Where(c => c.Id == id).FirstOrDefault();
            return Mapper.MapToNewsItem(newsItemDto);
        }

        public async Task<NewsItem> GetByIdWithItemsAsync(int id)
        {
            var newsItemDto = await _dbContext.News
                .Include(c => c.ChartItems)
                .Include(c => c.EmployeeNewsItems)
                .ThenInclude(c => c.EmployeeDTO)
               .Where(c => c.Id == id).FirstOrDefaultAsync();
            return Mapper.MapToNewsItem(newsItemDto);
        }
    }
}