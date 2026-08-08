using AssignmentLnp.Entities;
using AssignmentLnp.Interface;
using AssignmentLnp.Models;
using AssignmentLnp.Services.Interfaces;
using AutoMapper;

namespace AssignmentLnp.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentService(
            IDepartmentRepository departmentRepository,
            IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var departments = await _departmentRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<DepartmentDto>>(departments);
        }

        public async Task<DepartmentDto?> GetDepartmentByIdAsync(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);

            if (department == null)
                return null;

            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task CreateDepartmentAsync(DepartmentDto dto)
        {
            var department = _mapper.Map<Department>(dto);

            department.CreatedDate = DateTime.UtcNow;

            await _departmentRepository.AddAsync(department);
            await _departmentRepository.SaveChangesAsync();
        }
    }
}
