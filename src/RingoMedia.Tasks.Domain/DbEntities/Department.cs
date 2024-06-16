namespace RingoMedia.Tasks.Domain.DbEntities
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    public class Department
    {
        public Department(string name, string logoUrl, int? parentDepartmentId = null)
        {
            Name = name;
            LogoUrl = logoUrl;
            ParentDepartmentId = parentDepartmentId;
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LogoUrl { get; set; }

        [AllowNull]
        //Parent department Id
        public int? ParentDepartmentId { get; set; }
        //Parent department
        [AllowNull]
        public Department ParentDepartment { get; set; }
        //Sub departments
        [AllowNull]
        public ICollection<Department> SubDepartments { get; set; }
    }
}
