namespace SSquared.Lib.Data.Entities
{
    public class EmployeeRole : IEntity
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int RoleId { get; set; }

        public Employee? Employee { get; set; }

        public Role? Role { get; set; }
    }
}
