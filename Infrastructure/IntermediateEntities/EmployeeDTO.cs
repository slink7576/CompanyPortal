using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Infrastructure.IntermediateEntities
{
    public class EmployeeDTO : Employee
    {
        public ICollection<EmployeeNewsItem> EmployeeNewsItems { get; set; }
       
    }
}