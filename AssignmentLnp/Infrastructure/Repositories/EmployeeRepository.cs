using AssignmentLnp.Entities;
using AssignmentLnp.Infrastructure.Data;
using AssignmentLnp.Interface;
using Microsoft.EntityFrameworkCore;

namespace AssignmentLnp.Infrastructure.Repositories
{
    public class EmployeeRepository
       : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(
            string? search,
            int? departmentId,
            bool? isActive,
            string? sortBy,
            bool ascending,
            int pageNumber,
            int pageSize)
        {
            IQueryable<Employee> query = _context.Employees
                .Include(e => e.Department)
                .Where(e => !e.IsDeleted);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x =>
                    x.FirstName.Contains(search) ||
                    x.LastName.Contains(search) ||
                    x.Email.Contains(search));
            }

            if (departmentId.HasValue)
                query = query.Where(x => x.DepartmentId == departmentId);

            if (isActive.HasValue)
                query = query.Where(x => x.IsActive == isActive);

            query = sortBy?.ToLower() switch
            {
                "firstname" => ascending
                    ? query.OrderBy(x => x.FirstName)
                    : query.OrderByDescending(x => x.FirstName),

                "salary" => ascending
                    ? query.OrderBy(x => x.Salary)
                    : query.OrderByDescending(x => x.Salary),

                "joiningdate" => ascending
                    ? query.OrderBy(x => x.JoiningDate)
                    : query.OrderByDescending(x => x.JoiningDate),

                _ => query.OrderBy(x => x.Id)
            };

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
