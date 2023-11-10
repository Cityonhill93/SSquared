namespace SSquared.Lib.Data.Entities
{
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public IEnumerable<EmployeeRole> EmployeeRoles { get; set; } = new List<EmployeeRole>();

        public static Role Hero = new Role
        {
            Id = 1,
            Name = "Hero"
        };
        public static Role Vilian = new Role
        {
            Id = 2,
            Name = "Vilian"
        };
        public static Role Leader = new Role
        {
            Id = 3,
            Name = "Leader"
        };

        public static IEnumerable<Role> All = new List<Role>()
        {
            Hero,
            Vilian,
            Leader
        };
    }
}
