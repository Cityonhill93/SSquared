namespace SSquared.Lib.Data.Entities
{
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public IEnumerable<EmployeeRole> EmployeeRoles { get; set; } = new List<EmployeeRole>();
    }
}
