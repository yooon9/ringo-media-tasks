namespace RingoMedia.Tasks.Application.Dto
{
    public class DepartmentHierarchyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentDepartmentId { get; set; }
        public int? Level { get; set; }
        public List<DepartmentHierarchyDto> SubDepartments { get; set; }
    }
}
