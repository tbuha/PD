﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebPD.API.Entities;
using WebPD.API.Entities.Repositories;

namespace WebPD.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/employees")]
    public class EmployeesController : ApiController
    {

        private EmployeeRepository _employeeRepository;
        public EmployeesController()
        {
            _employeeRepository = new EmployeeRepository();
        }

        // GET api/Employees                
        [HttpGet]
        public IEnumerable<Employee> Get(string firstName, string lastName, string city, string country, string extension)
        {

            string _firstName = SafeLower(firstName);
            string _lastName = SafeLower(lastName);
            string _city = SafeLower(city);
            string _country = SafeLower(country);
            string _extension = SafeLower(extension);

            var searchEmployee = from employee in _employeeRepository.List()
                                 where employee.FirstName.ToLower().Contains(_firstName)
                                     && employee.LastName.ToLower().Contains(_lastName)
                                     && employee.City.ToLower().Contains(_city)
                                     && employee.Country.ToLower().Contains(_country)
                                     && (_extension == "" || employee.Extension == _extension)
                                 select employee;
            return searchEmployee;


        }

        // GET api/Employees                
        /// <summary>
        /// get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]        
        public Employee Get(int id)
        {
            var searchEmployee = from employee in _employeeRepository.List()
                                 where employee.EmployeeID == id
                                 select employee;
            return searchEmployee.FirstOrDefault();
        }

        private string SafeLower<T>(T value)
        {
            if (value == null) return "";
            else if (value is string)
            {
                var str = value as string;
                return string.IsNullOrEmpty(str) ? "" : str.ToLower();
            }
            else
            {
                return value.ToString();
            }
        }

        [HttpPut]
        public void Edit([FromBody]Employee employee)
        {
            EntitiesContext context = new EntitiesContext();
            Employee employeeToEdit = context.Employees.Find(employee.EmployeeID);
            context.Employees.Remove(employeeToEdit);
            var updatedEmployee = employee;
            context.Employees.Add(updatedEmployee);

            context.SaveChanges();
        }
    }
}
