using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class EmployeeListViewModel
    {
        public IEnumerable<Employee> Employers { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public Position? CurrentPosition { get; set; }
        public string CurrentName { get; set; }
       public bool IsDesc { get; set; }
    }
}
