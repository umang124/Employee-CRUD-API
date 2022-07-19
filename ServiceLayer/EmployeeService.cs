using DataLayer.Data;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.DTOs;
using ServiceLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IEmployeeService
    {
        Task AddEmployee(EmployeeDTO employee);
        Task UpdateEmployee(int id, EmployeeUpdateDTO employee);
        Task DeleteEmployee(int id);
        Task<ICollection<EmployeeView>> GetEmployees();
        Task<EmployeeView> GetEmployee(int id);
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeDbContext _dbContext;
        public EmployeeService(EmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddEmployee(EmployeeDTO employee)
        {
            Employee employeeMap = new();
            employeeMap.Name = employee.Name;
            employeeMap.Address = employee.Address;
            employeeMap.Description = employee.Description;

            await _dbContext.Employees.AddAsync(employeeMap);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteEmployee(int id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);

            if (employee != null)
            {
                _dbContext.Employees.Remove(employee);
                await _dbContext.SaveChangesAsync();
            }             
            
        }

        public async Task<EmployeeView> GetEmployee(int id)
        {
            var employee = await _dbContext.Employees.Select(x => new EmployeeView
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address
            }).FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {
                return employee;
            }
            return new EmployeeView();
        }

        public async Task<ICollection<EmployeeView>> GetEmployees()
        {
            var employees = await _dbContext.Employees.Select(x => new EmployeeView
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address
            }).ToListAsync();

            return employees;
        }

        public async Task UpdateEmployee(int id, EmployeeUpdateDTO employee)
        {
            var getemployee = await _dbContext.Employees.FindAsync(id);
            if (getemployee != null)
            {
                getemployee.Name = employee.Name;
                getemployee.Address = employee.Address;

                await _dbContext.SaveChangesAsync();
            }                      
        }
    }
}
