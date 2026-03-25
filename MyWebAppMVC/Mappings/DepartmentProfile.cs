using AutoMapper;
using MyWebAppMVC.Contracts.ViewModels.Departments;
using MyWebAppMVC.Models;

namespace MyWebAppMVC.Mappings;

public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        // Convention-based: Id → Id, Name → Name
        CreateMap<Department, DepartmentDto>();
    }
}