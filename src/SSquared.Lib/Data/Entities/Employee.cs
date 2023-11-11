using SSquared.Lib.Arguments;

namespace SSquared.Lib.Data.Entities
{
    public class Employee : IEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string EmployeeId { get; set; } = default!;

        public int? ManagerId { get; set; }

        public Employee? Manager { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();

        public ICollection<EmployeeRole> EmployeeRoles { get; set; } = new List<EmployeeRole>();

        public bool MayBeManagedBy(Employee potentialManager)
        {
            /*
            TODO: There is a potential issue here where the potential manager could the a grandchild of the employee in question.
            A potential fix would be to turn on lazy loading and recursively check the collection of employees. The downside is an unknown number of DB hits :(
            Alternatively, we could create a service which does a better job of getting employees recursively (similar to the org chart) and makes sure that all is good.
            Due to the short time before the interview, this may or may not be fixed, but I wanted to call it out as a known thing.
            */


            if (potentialManager.Id == Id)
            {
                //You cannot manaage yourself
                return false;
            }
            else if (Employees.Any(e => e.Id == potentialManager.Id))
            {
                //You may not be managed by one of your own employees
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Modify(ModifyEmployeeArguments args)
        {
            FirstName = args.FirstName;
            LastName = args.LastName;
            EmployeeId = args.EmployeeId;
            ManagerId = args.ManagerId;

            var rolesToRemove = EmployeeRoles
                .Where(r => !args.RoleIds.Contains(r.RoleId))
                .ToList();
            foreach (var role in rolesToRemove)
            {
                EmployeeRoles.Remove(role);
            }

            var rolesToAdd = args
                .RoleIds
                .Where(id => !EmployeeRoles.Any(role => role.Id == id))
                .Select(id => new EmployeeRole
                {
                    RoleId = id
                })
                .ToList();
            foreach (var role in rolesToAdd)
            {
                EmployeeRoles.Add(role);
            }
        }
    }
}
