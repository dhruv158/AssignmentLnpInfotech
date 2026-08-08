using AssignmentLnp.Entities;
using AssignmentLnp.Models;
using AutoMapper;

namespace AssignmentLnp.Mapper
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
         

            CreateMap<EmployeeDto, Employee>();

        

            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.DepartmentName,
                    opt => opt.MapFrom(src => src.Department.Name));

         
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentDto, Department>();

         
        }
    }
}
