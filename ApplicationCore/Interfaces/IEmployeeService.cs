using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IEmployeeService
    {
        Task Delete(int employeeId);
        Task Create(Employee entity);
        Task Update(int employeeId, Employee updateEntity);
    }
}