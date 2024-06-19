namespace RingoMedia.Tasks.Application.Services.DepartmentServices
{
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using RingoMedia.Tasks.Application.Dto;
    using RingoMedia.Tasks.Application.Models;
    using RingoMedia.Tasks.Domain.Context;
    using RingoMedia.Tasks.Domain.DbEntities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class DepartmentService : IDepartmentService
    {
        private readonly RingoMediaDbContext _context;

        public DepartmentService(RingoMediaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            return await _context.Departments.AsNoTracking().ToListAsync();
        }

        public async Task<DepartmentDetailsDto> GetDepartmentAsync(int id)
        {
            var department = await _context.Departments
              .AsNoTracking()
              .FirstOrDefaultAsync(d => d.Id == id);

            if (department is null)
                return null;

            return new DepartmentDetailsDto()
            {
                Id = department.Id,
                Name = department.Name,
                LogoUrl = department.LogoUrl,
                ParentDepartmentId = department.Id,
                Parents = await GetDepartmentHierarchyAsync(id),
                SubDepartments = await GetRecursiveDepartmentHierarchy(id, department.ParentDepartmentId) ?? new List<DepartmentHierarchyDto>()
            };
        }

        public async Task<int> CreateDepartmentAsync(CreateDepartmentModel departmentModel)
        {
            var department = new Department(departmentModel.Name, departmentModel.LogoUrl, departmentModel.ParentDepartmentId);
            _context.Add(department);
            await _context.SaveChangesAsync();
            return department.Id;
        }

        public async Task<List<Department>> GetSubDepartmentsAsync(int? id)
        {
            return await _context.Departments
                .AsNoTracking()
                .Where(d => d.ParentDepartmentId == id)
                .ToListAsync();
        }

        #region Helpers
        private async Task<List<DepartmentHierarchyDto>> GetDepartmentHierarchyAsync(int departmentId)
        {
            //SQL Server (CTE) Expression
            var query = @"
            WITH DepartmentHierarchy AS (
                SELECT 
                    Id,
                    Name,
                    ParentDepartmentId,
                    0 AS Level
                FROM 
                    Departments
                WHERE 
                    Id = @DepartmentId

                UNION ALL

                SELECT 
                    d.Id,
                    d.Name,
                    d.ParentDepartmentId,
                    dh.Level + 1 AS Level
                FROM 
                    Departments d
                INNER JOIN 
                    DepartmentHierarchy dh ON d.Id = dh.ParentDepartmentId
            )
            SELECT 
                Id,
                Name,
                ParentDepartmentId,
                Level
            FROM 
                DepartmentHierarchy
            ORDER BY 
                Level DESC;
        ";

            var parameters = new SqlParameter("@DepartmentId", departmentId);
            var departmentHierarchy = new List<DepartmentHierarchyDto>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.Add(parameters);
                _context.Database.OpenConnection();

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        departmentHierarchy.Add(new DepartmentHierarchyDto
                        {
                            Id = result.GetInt32(0),
                            Name = result.GetString(1),
                            ParentDepartmentId = result.IsDBNull(2) ? (int?)null : result.GetInt32(2),
                            Level = result.GetInt32(3)
                        });
                    }
                }
            }

            return departmentHierarchy;
        }

        #region Department Tree
        private async Task<List<DepartmentHierarchyDto>> GetRecursiveDepartmentHierarchy(int departmentId, int? parentId)
        {
            var departments = new List<DepartmentHierarchyDto>();
            var connectionString = _context.Database.GetDbConnection().ConnectionString;
            var query = @"
    WITH RecursiveCTE AS (
        SELECT 
            Id, 
            Name, 
            ParentDepartmentId, 
            0 AS Level
        FROM 
            Departments
        WHERE 
            id = @departmentId

        UNION ALL

        SELECT 
            d.Id, 
            d.Name, 
            d.ParentDepartmentId, 
            r.Level + 1
        FROM 
            Departments d
        INNER JOIN 
            RecursiveCTE r 
        ON 
            d.ParentDepartmentId = r.Id
    )
    SELECT 
        Id, 
        Name, 
        ParentDepartmentId, 
        Level
    FROM 
        RecursiveCTE
    ORDER BY 
        Level, 
        ParentDepartmentId, 
        Id;
";

            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new SqlParameter("@departmentId", departmentId);
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(parameters);
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            departments.Add(new DepartmentHierarchyDto
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                ParentDepartmentId = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                                Level = reader.GetInt32(3)
                            });
                        }
                    }
                }
            }
            var buildTree = BuildTree(departments, parentId);
            return buildTree;
        }
        private List<DepartmentHierarchyDto> BuildTree(List<DepartmentHierarchyDto> flatList, int? parentId)
        {
            var lookup = flatList.ToLookup(d => d.ParentDepartmentId);
            foreach (var item in flatList)
            {
                item.SubDepartments = lookup[item.Id].ToList();
            }

            // If startParentId is null, it means we are starting from the top-level departments
            if (parentId is null)
            {
                return lookup[null].ToList();
            }
            else
            {
                return lookup[parentId].ToList();
            }
        }
        #endregion
        #endregion
    }
}
