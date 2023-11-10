namespace SSquared.App.DTO
{
    public record ExpandedEmployeeDto : EmployeeDto
    {
        public ExpandedEmployeeDto(
            int id,
            string firstName,
            string lastName,
            string employeeId,
            EmployeeDto? manager,
            IEnumerable<EmployeeDto> employees,
            IEnumerable<RoleDto> roles,
            Uri getUrl)
            : base(id, firstName, lastName, employeeId, getUrl)
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
