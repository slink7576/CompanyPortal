using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ApplicationCore.Entities
{
    public class NewsItem : BaseEntity
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public string PhotoURL { get; set; }
        public string Author { get; set; }
        public DateTime Created { get; set; }
        public int TotalLikes { get; set; }
        public List<ChartItem> ChartItems { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}