using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.IntermediateEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class EmployeeRepository : EfRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public override Employee Add(Employee employee)
        {
            _dbContext.Employers.Add(Mapper.MapToEmployeeDto(employee));
            _dbContext.SaveChanges();
            return employee;
        }

        public override async Task<Employee> AddAsync(Employee employee)
        {
            await _dbContext.Employers.AddAsync(Mapper.MapToEmployeeDto(employee));
            await  _dbContext.SaveChangesAsync();
            return employee;
        }

        public override void Update(Employee employee)
        {
            var result = Mapper.MapToEmployeeDto(employee);
            var source = _dbContext.Employers
                .Include(c => c.EmployeeNewsItems)
               .Where(c => c.Id == employee.Id).FirstOrDefault();
            source.Name = employee.Name;
            source.PhotoURL = employee.PhotoURL;
            source.Position = employee.Position;
            source.Surname = employee.Surname;
            source.EmployeeNewsItems = result.EmployeeNewsItems;
            _dbContext.SaveChanges();
        }

        public override async Task UpdateAsync(Employee employee)
        {
            var result = Mapper.MapToEmployeeDto(employee);
            var source = await _dbContext.Employers
                .Include(c => c.EmployeeNewsItems)
               .Where(c => c.Id == employee.Id).FirstOrDefaultAsync();
            source.Name = employee.Name;
            source.PhotoURL = employee.PhotoURL;
            source.Position = employee.Position;
            source.Surname = employee.Surname;
            source.EmployeeNewsItems = result.EmployeeNewsItems;
            await _dbContext.SaveChangesAsync();
        }

        public override void Delete(Employee employee)
        {
            _dbContext.Employers.Remove(_dbContext.Employers.Find(employee.Id));
            _dbContext.SaveChanges();
        }

        public override async Task DeleteAsync(Employee employee)
        {
            _dbContext.Employers.Remove(await _dbContext.Employers.FindAsync(employee.Id));
            await _dbContext.SaveChangesAsync();
        }


      

        public async Task<Employee> GetByIdWithItemsAsync(int id)
        {
            var employeeDto = await _dbContext.Employers.Include(c => c.EmployeeNewsItems)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            return Mapper.MapToEmployee(employeeDto);
        }
        public async Task<Employee> GetByEmailAsync(string email)
        { 
            var employeeDto = await _dbContext.Employers.Include(c => c.EmployeeNewsItems)
                .Where(c => c.Email == email)
                .FirstOrDefaultAsync();
            return Mapper.MapToEmployee(employeeDto);
        }

        public Employee GetByEmail(string email)
        {
            return Mapper.MapToEmployee(_dbContext.Employers.Include(c => c.EmployeeNewsItems)
                .Where(c => c.Email == email)
                .FirstOrDefault());
        }
    }
}