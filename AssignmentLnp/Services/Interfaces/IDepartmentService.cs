using AssignmentLnp.Models;

namespace AssignmentLnp.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();

        Task<DepartmentDto?> GetDepartmentByIdAsync(int id);

        Task CreateDepartmentAsync(DepartmentDto dto);
    }
}
