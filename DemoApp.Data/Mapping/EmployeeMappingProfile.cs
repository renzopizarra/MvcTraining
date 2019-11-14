using AutoMapper;
using DemoApp.Data.Models;
using DemoApp.Model;

namespace DemoApp.Data.Mapping
{
    public class EmployeeMappingProfile:Profile
    {
        public EmployeeMappingProfile()
        {
            //CreateMap<Source, Destination>
            CreateMap<Employee, ModelEmployee>()
                .ReverseMap();
        }
    }
}
