using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Infrastructure.IntermediateEntities
{
    public class EmployeeNewsItem
    {
        public NewsItemDTO NewsItemDTO { get; set; }
        public EmployeeDTO EmployeeDTO { get; set; }
        public int EmployeeId { get; set; }
        public int NewsItemId { get; set; }
    }
}