using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IAsyncRepository<Employee> _EmployeeRepository;

        public EmployeeService(IAsyncRepository<Employee> EmployeeRepository)
        {
            _EmployeeRepository = EmployeeRepository;
        }

        public async Task Create(Employee entity)
        {
            await _EmployeeRepository.AddAsync(entity);
        }

        public async Task Delete(int employeeId)
        {
            var employeeToDelete = await _EmployeeRepository.GetByIdAsync(employeeId);
            if(employeeToDelete == null)
                throw new ArgumentNullException();
            await _EmployeeRepository.DeleteAsync(employeeToDelete);
        }

        public async Task Update(int employeeId, Employee updateEntity)
        {
            var employeeToUpdate = await _EmployeeRepository.GetByIdAsync(employeeId);
            if (employeeToUpdate == null)
                throw new ArgumentNullException();
            employeeToUpdate.Name = updateEntity.Name;
            employeeToUpdate.Surname = updateEntity.Surname;
            employeeToUpdate.PhotoURL = updateEntity.PhotoURL;
            employeeToUpdate.PhoneNumber = updateEntity.PhoneNumber;
            employeeToUpdate.Email = updateEntity.Email;
            employeeToUpdate.Position = updateEntity.Position;
            await _EmployeeRepository.UpdateAsync(employeeToUpdate);
        }
    }
}