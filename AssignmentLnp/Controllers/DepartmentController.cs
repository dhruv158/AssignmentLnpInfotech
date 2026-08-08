using AssignmentLnp.Models;
using AssignmentLnp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentLnp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();

            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            
            if (id <= 0)
                return BadRequest("Department ID must be greater than 0.");

            var department = await _departmentService.GetDepartmentByIdAsync(id);

            if (department == null)
                return NotFound("Department not found.");

            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentDto dto)
        {
           
            if (dto == null)
                return BadRequest("Department data is required.");

            await _departmentService.CreateDepartmentAsync(dto);

            return Ok("Department created successfully.");
        }
    }
}