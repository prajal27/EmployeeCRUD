using AutoMapper;
using EmployeeManagement.Dto;
using EmployeeManagement.Models;

namespace EmployeeManagement.AutoMapper
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDTO>();
            /*.ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.DepartmentName : string.Empty));
            CreateMap<EmployeeDTO, Employee>();*/

            CreateMap<EmployeeDTO, Employee>()
            .ForMember(dest => dest.DepartmentID, opt => opt.MapFrom(src => src.DepartmentId));

        }
    }
}
