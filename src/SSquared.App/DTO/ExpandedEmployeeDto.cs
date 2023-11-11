namespace SSquared.App.DTO
{
    public record ExpandedEmployeeDto : EmployeeDto
    {
        public ExpandedEmployeeDto(
            EmployeeDto baseDto,
            EmployeeDto? manager,
            IEnumerable<EmployeeDto> employees,
            IEnumerable<RoleDto> roles)
            : base(baseDto)
        {
            Employees = employees;
            Manager = manager;
            Roles = roles;
        }

        public IEnumerable<EmployeeDto> Employees { get; }

        public EmployeeDto? Manager { get; }

        public IEnumerable<RoleDto> Roles { get; }
    }
}
