using AssignmentLnp.Entities;
using AssignmentLnp.Interface;
using AssignmentLnp.Models;
using AssignmentLnp.Services.Interfaces;
using AutoMapper;
namespace AssignmentLnp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(
            string? search,
            int? departmentId,
            bool? isActive,
            string? sortBy,
            bool ascending,
            int pageNumber,
            int pageSize)
        {
            var employees = await _employeeRepository.GetEmployeesAsync(
                search,
                departmentId,
                isActive,
                sortBy,
                ascending,
                pageNumber,
                pageSize);

            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return null;

            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task CreateEmployeeAsync(EmployeeDto dto)
        {
            var employee = _mapper.Map<Employee>(dto);

            employee.CreatedDate = DateTime.UtcNow;
            employee.IsDeleted = false;

            await _employeeRepository.AddAsync(employee);
            await _employeeRepository.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(EmployeeDto dto)
        {
            var employee = await _employeeRepository.GetByIdAsync(dto.Id);

            if (employee == null)
                throw new Exception("Employee not found.");

            _mapper.Map(dto, employee);

            employee.UpdatedDate = DateTime.UtcNow;

            _employeeRepository.Update(employee);

            await _employeeRepository.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                throw new Exception("Employee not found.");

            employee.IsDeleted = true;

            _employeeRepository.Update(employee);

            await _employeeRepository.SaveChangesAsync();
        }
    }
}
