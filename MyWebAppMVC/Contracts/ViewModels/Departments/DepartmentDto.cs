namespace MyWebAppMVC.Contracts.ViewModels.Departments
{
    public record DepartmentDto(int Id, string Name);
    public record DepartmentCreateDto(string Name);
}