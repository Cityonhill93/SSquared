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

        public ExpandedEmployeeDto(
            int id,
            string firstName,
            string lastName,
            string employeeId,
            EmployeeDto? manager,
            IEnumerable<EmployeeDto> employees,
            IEnumerable<RoleDto> roles,
            Uri getUrl,
            Uri updateUrl,
            Uri viewUrl)
            : this(new EmployeeDto(id, firstName, lastName, employeeId, getUrl, updateUrl, viewUrl), manager, employees, roles)
        {

        }

        public IEnumerable<EmployeeDto> Employees { get; }

        public EmployeeDto? Manager { get; }

        public IEnumerable<RoleDto> Roles { get; }
    }
}
