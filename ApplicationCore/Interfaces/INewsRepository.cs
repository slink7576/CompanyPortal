using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
   public interface INewsRepository : IRepository<NewsItem>, IAsyncRepository<NewsItem>
    {
        NewsItem GetByIdWithItems(int id);
        Task<NewsItem> GetByIdWithItemsAsync(int id);
    }
}