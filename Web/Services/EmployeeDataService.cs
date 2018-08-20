using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Services
{
    public class EmployeeDataService
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeDataService(IEmployeeService employeeService, IEmployeeRepository employeeRepository)
        {
            _employeeService = employeeService;
            _employeeRepository = employeeRepository;
        }

        public async Task AddAsync(Employee employee)
        {
            await _employeeRepository.AddAsync(employee);
        }
        public async Task<Employee> GetByEmailAsync(string email)
        {
            return await _employeeRepository.GetByEmailAsync(email);
        }
        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }
        public async Task DeleteAsync(Employee employee)
        {
            await _employeeRepository.DeleteAsync(employee);
        }
        public async Task<List<Employee>> ListAllAsync()
        {
            return await _employeeRepository.ListAllAsync();
        }
        public async Task UpdateAsync(int id, Employee employee)
        {
            await _employeeService.Update(id, employee);
        }
    }
}
