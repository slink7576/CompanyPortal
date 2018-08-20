using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ApplicationCore.Entities
{
    public class ChartItem : BaseEntity
    {
    
        public DateTime Date { get; set; }
        public int Likes { get; set; }
    }
}