using AssignmentLnp.Models;

namespace AssignmentLnp.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(
            string? search,
            int? departmentId,
            bool? isActive,
            string? sortBy,
            bool ascending,
            int pageNumber,
            int pageSize);

        Task<EmployeeDto?> GetEmployeeByIdAsync(int id);

        Task CreateEmployeeAsync(EmployeeDto dto);

        Task UpdateEmployeeAsync(EmployeeDto dto);

        Task DeleteEmployeeAsync(int id);
    }
}
