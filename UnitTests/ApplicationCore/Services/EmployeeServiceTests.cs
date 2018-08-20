using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.ApplicationCore.Services
{
    public class EmployeeServiceTests
    {
        private Mock<IAsyncRepository<Employee>> _EmployeeRepository;
        private void Arrange()
        {
            _EmployeeRepository = new Mock<IAsyncRepository<Employee>>();
        }

        [Fact]
        public async Task CreateTest()
        {
            Arrange();
            var employeeService = new EmployeeService(_EmployeeRepository.Object);
            var testedEmployee = new Employee { Name = "Alex" };
            await employeeService.Create(testedEmployee);
            _EmployeeRepository.Verify(c => c.AddAsync(It.Is<Employee>(arg => arg.Name == "Alex")));
        }

        [Fact]
        public async Task DeleteTest()
        {
            Arrange();
            var employeeService = new EmployeeService(_EmployeeRepository.Object);
            var testedEmployee = new Employee { Name = "Alex", Id = 1 };
            _EmployeeRepository.Setup(c => c.GetByIdAsync(1)).Returns(Task.FromResult(testedEmployee));
            await employeeService.Delete(1);
            _EmployeeRepository.Verify(c => c.DeleteAsync(It.Is<Employee>(arg => arg.Name == "Alex" && arg.Id == 1)));
        }

        [Fact]
        public async Task DeleteWrongIdTest()
        {
            Arrange();
            var employeeService = new EmployeeService(_EmployeeRepository.Object);
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await employeeService.Delete(-1));
        }
        [Fact]
        public async Task UpdateWrongIdTest()
        {
            Arrange();
            var employeeService = new EmployeeService(_EmployeeRepository.Object);
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await employeeService.Update(-1, new Employee()));
        }

        [Fact]
        public async Task UpdateTest()
        {

            Arrange();
            var employeeService = new EmployeeService(_EmployeeRepository.Object);
            var testedEmployee = new Employee
            {
                Name = "1",
                Email = "1",
                NewsItems = new List<NewsItem>(),
                PhoneNumber = "1",
                PhotoURL = "1",
                Position = Position.Accountant,
                Surname = "1",
                Id = 1
            };
            var requiredEmployee = new Employee
            {
                Name = "name",
                Email = "email",
                NewsItems = new List<NewsItem>(),
                PhoneNumber = "112233",
                PhotoURL = "url",
                Position = Position.SoftwareEngineer,
                Surname = "surname",
                Id = 1
            };

            _EmployeeRepository.Setup(c => c.GetByIdAsync(1)).Returns(Task.FromResult(testedEmployee));

            await employeeService.Update(1, requiredEmployee);

            _EmployeeRepository.Verify(c => c.UpdateAsync(It.Is<Employee>(arg => 
            arg.Name == "name" && 
            arg.Id == 1 &&
            arg.Email == "email" &&
            arg.PhoneNumber == "112233" &&
            arg.PhotoURL == "url" &&
            arg.Position == Position.SoftwareEngineer &&
            arg.Surname == "surname"
            )));
        }
    }
}