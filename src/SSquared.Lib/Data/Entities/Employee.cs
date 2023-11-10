namespace SSquared.Lib.Data.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string EmployeeId { get; set; } = default!;

        public int? ManagerId { get; set; }

        public Employee? Manager { get; set; }

        public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();

        public IEnumerable<EmployeeRole> EmployeeRoles { get; set; } = new List<EmployeeRole>();
    }
}
