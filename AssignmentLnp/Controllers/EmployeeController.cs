using AssignmentLnp.Models;
using AssignmentLnp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentLnp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            string? search,
            int? departmentId,
            bool? isActive,
            string? sortBy,
            bool ascending = true,
            int pageNumber = 1,
            int pageSize = 10)
        {
            
            if (pageNumber <= 0)
                return BadRequest("Page number must be greater than 0.");

            if (pageSize <= 0)
                return BadRequest("Page size must be greater than 0.");

            var employees = await _employeeService.GetAllEmployeesAsync(
                search,
                departmentId,
                isActive,
                sortBy,
                ascending,
                pageNumber,
                pageSize);

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Employee ID must be greater than 0.");

            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
                return NotFound("Employee not found.");

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeDto dto)
        {
            if (dto == null)
                return BadRequest("Employee data is required.");

            await _employeeService.CreateEmployeeAsync(dto);

            return Ok("Employee created successfully.");
        }

        [HttpPut]
        public async Task<IActionResult> Update(EmployeeDto dto)
        {
            if (dto == null)
                return BadRequest("Employee data is required.");

            if (dto.Id <= 0)
                return BadRequest("Employee ID must be greater than 0.");

            await _employeeService.UpdateEmployeeAsync(dto);

            return Ok("Employee updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Employee ID must be greater than 0.");

            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
                return NotFound("Employee not found.");

            await _employeeService.DeleteEmployeeAsync(id);

            return Ok("Employee deleted successfully.");
        }
    }
}