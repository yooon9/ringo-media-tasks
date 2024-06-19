namespace RingoMedia.Tasks.Application.Services.DepartmentServices
{
    using RingoMedia.Tasks.Application.Dto;
    using RingoMedia.Tasks.Application.Models;
    using RingoMedia.Tasks.Domain.DbEntities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDepartmentService
    {
        Task<int> CreateDepartmentAsync(CreateDepartmentModel departmentModel);
        Task<DepartmentDetailsDto> GetDepartmentAsync(int id);
        Task<List<Department>> GetDepartmentsAsync();
        Task<List<DepartmentHierarchyDto>> GetRecursiveDepartmentHierarchy(int? departmentId = null, int? parentId = null);
        Task<List<Department>> GetSubDepartmentsAsync(int? id);
    }
}
