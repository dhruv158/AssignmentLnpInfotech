using AssignmentLnp.Entities;

namespace AssignmentLnp.Interface
{
   
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync(
            string? search,
            int? departmentId,
            bool? isActive,
            string? sortBy,
            bool ascending,
            int pageNumber,
            int pageSize);


    }
}
