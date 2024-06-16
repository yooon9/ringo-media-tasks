namespace RingoMedia.Tasks.Application.Dto
{
    using System.Collections.Generic;

    public class DepartmentDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public int? ParentDepartmentId { get; set; }
        public List<DepartmentHierarchyDto> Parents { get; set; }
    }
}
