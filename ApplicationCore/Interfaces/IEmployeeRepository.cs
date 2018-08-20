using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>, IAsyncRepository<Employee>
    {
        Task<Employee> GetByEmailAsync(string email);
        Employee GetByEmail(string email);
        Task<Employee> GetByIdWithItemsAsync(int id);
    }
}