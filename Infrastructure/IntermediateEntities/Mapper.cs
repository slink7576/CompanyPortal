using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IntermediateEntities
{
    public class Mapper 
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly INewsRepository _newsRepository;

        public Mapper(IEmployeeRepository employeeRepository, INewsRepository newsRepository)
        {
            _employeeRepository = employeeRepository;
            _newsRepository = newsRepository;
        }



        public static NewsItemDTO MapToNewsItemDto(NewsItem newsItem)
        {
            var result = new NewsItemDTO();
            result.EmployeeNewsItems = new List<EmployeeNewsItem>();
            if (newsItem.Employees != null)
            {
                foreach (Employee c in newsItem.Employees)
                {
                    result.EmployeeNewsItems.Add(
                        new EmployeeNewsItem
                        {
                          
                            NewsItemId = newsItem.Id,
                            EmployeeId = c.Id
                        });
                }
            }
            
            result.Author = newsItem.Author;
            result.Body = newsItem.Body;
            result.Created = newsItem.Created;
            result.Header = newsItem.Header;
            result.PhotoURL = newsItem.PhotoURL;
            result.Id = newsItem.Id;
            result.ChartItems = newsItem.ChartItems;
            result.TotalLikes = newsItem.TotalLikes;
            result.Employees = newsItem.Employees;
            return result;
        }

        public static EmployeeDTO MapToEmployeeDto(Employee employee)
        {
            var result = new EmployeeDTO();
            result.EmployeeNewsItems = new List<EmployeeNewsItem>();
            if (employee.NewsItems != null)
            {
               
                foreach (NewsItem c in employee.NewsItems)
                {
                    result.EmployeeNewsItems.Add(
                        new EmployeeNewsItem
                        {
                            
                            NewsItemId = c.Id,
                            EmployeeId = employee.Id,
                        });
                }
            }
            result.Email = employee.Email;
            result.PhoneNumber = employee.PhoneNumber;
            result.Name = employee.Name;
            result.Surname = employee.Surname;
            result.PhotoURL = employee.PhotoURL;
            result.Position = employee.Position;
            result.Id = employee.Id;

            return result;
        }



       
 
        public static NewsItem MapToNewsItem(NewsItemDTO newsItem)
        {
            var result = new NewsItem
            {
                Author = newsItem.Author,
                Body = newsItem.Body,
                Created = newsItem.Created,
                Header = newsItem.Header,
                PhotoURL = newsItem.PhotoURL,
                Id = newsItem.Id,
                ChartItems = newsItem.ChartItems,
                TotalLikes = newsItem.TotalLikes
            };
            result.Employees = new List<Employee>();
            if (newsItem.EmployeeNewsItems != null)
            {


                foreach (EmployeeNewsItem c in newsItem.EmployeeNewsItems)
                {
                    result.Employees.Add(new Employee { Id = c.EmployeeId });

                }
            }
            return result;
        }
        public static Employee MapToEmployee(EmployeeDTO employee)
        {
            var result = new Employee
            {
                Id = employee.Id,
                Name = employee.Name,
                Surname = employee.Surname,
                Position = employee.Position,
                PhotoURL = employee.PhotoURL
            };
            result.NewsItems = new List<NewsItem>();
            if (employee.EmployeeNewsItems != null)
            {
               
                foreach(EmployeeNewsItem c in employee.EmployeeNewsItems)
                {
                    result.NewsItems.Add(new NewsItem { Id = c.NewsItemId});
                       
                }
            }
            return result;
        }
    }
}