using AssignmentLnp.Entities;
using AssignmentLnp.Infrastructure.Data;
using AssignmentLnp.Interface;

namespace AssignmentLnp.Infrastructure.Repositories
{

    public class DepartmentRepository
        : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
