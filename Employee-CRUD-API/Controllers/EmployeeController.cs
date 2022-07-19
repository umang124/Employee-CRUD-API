using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using ServiceLayer.DTOs;
using ServiceLayer.ViewModels;

namespace Employee_CRUD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeDTO employee)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.AddEmployee(employee);
                return Ok("Added");
            }
            else
            {
                return BadRequest(ModelState);
            }           
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeUpdateDTO employeeUpdate)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.UpdateEmployee(id, employeeUpdate);
                return Ok("Updated");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeService.DeleteEmployee(id);
            return Ok("Deleted");
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeService.GetEmployees();
            return Ok(employees);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _employeeService.GetEmployee(id);
            return Ok(employee);
        }
    }
}
