using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface INewsService
    {
        Task DeleteNewsItem(int newsItemId);
        Task CreateNewsItem(NewsItem entity);
        Task UpdateNewsItem(int newsItemId, string header, string body, string author, string photoUrl);
        Task<IEnumerable<ChartItem>> GetNewsChart(int newsItemId);
        Task Like(int newsItemId, Employee sender);
    }
}