using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class NewsItemListViewModel
    {
        public IEnumerable<NewsItem> News { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
