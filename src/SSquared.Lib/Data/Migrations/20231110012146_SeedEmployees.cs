using Microsoft.EntityFrameworkCore.Migrations;
using SSquared.Lib.Data.Entities;

#nullable disable

namespace SSquared.Lib.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedEmployees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var currentId = 1;

            //Bat-family
            var bruceId = currentId++;
            SeedEmployee(bruceId, "Bruce", "Wayne", "BF00000001", null, new List<Role> { Role.Hero, Role.Leader });
            SeedEmployee(currentId++, "Robert", "Grayson", "BF00000002", bruceId, new List<Role> { Role.Hero });
            SeedEmployee(currentId++, "Barbra", "Gordon", "BF00000003", bruceId, new List<Role> { Role.Hero });
            SeedEmployee(currentId++, "Jason", "Todd", "BF00000004", bruceId, new List<Role> { Role.Hero });
            SeedEmployee(currentId++, "Tim", "Drake", "BM00000005", bruceId, new List<Role> { Role.Hero });
            SeedEmployee(currentId++, "Damien", "Wayne", "BM00000006", bruceId, new List<Role> { Role.Hero });

            //Galactic Empire
            var palpsId = currentId++;
            SeedEmployee(palpsId, "Sheev", "Palpatine", "GE00000001", null, new List<Role> { Role.Vilian, Role.Leader });

            var anakinId = currentId++;
            SeedEmployee(anakinId, "Anakin", "Skywalker", "GE00000002", palpsId, new List<Role> { Role.Vilian, Role.Leader });


            var starkillerId = currentId++;
            SeedEmployee(starkillerId, "Galan", "Merek", "GE00000003", anakinId, new List<Role> { Role.Hero, Role.Vilian, Role.Leader });
            SeedEmployee(currentId++, "Juno", "Eclipse", "GE00000004", starkillerId, new List<Role> { Role.Hero, Role.Vilian });

            SeedEmployee(currentId++, "Mas", "Amedda", "GE00000005", palpsId, new List<Role> { Role.Vilian });
            SeedEmployee(currentId++, "Mara", "Jade", "GE00000006", palpsId, new List<Role> { Role.Hero, Role.Vilian });

            var grandInquisitorId = currentId++;
            SeedEmployee(grandInquisitorId, "Grand", "Inquisitor", "GE00000007", anakinId, new List<Role> { Role.Leader, Role.Vilian });
            SeedEmployee(currentId++, "Second", "Sister", "GE00000008", grandInquisitorId, new List<Role> { Role.Vilian });
            SeedEmployee(currentId++, "Third", "Sister", "GE00000009", grandInquisitorId, new List<Role> { Role.Vilian });
            SeedEmployee(currentId++, "Fifth", "Brother", "GE00000010", grandInquisitorId, new List<Role> { Role.Vilian });
            SeedEmployee(currentId++, "Sixth", "Brother", "GE00000011", grandInquisitorId, new List<Role> { Role.Vilian });
            SeedEmployee(currentId++, "Seventh", "Sister", "GE00000012", grandInquisitorId, new List<Role> { Role.Vilian });
            SeedEmployee(currentId++, "Eighth", "Brother", "GE00000013", grandInquisitorId, new List<Role> { Role.Vilian });
            SeedEmployee(currentId++, "Ninth", "Sister", "GE00000014", grandInquisitorId, new List<Role> { Role.Vilian });
            SeedEmployee(currentId++, "Tenth", "Brother", "GE00000015", grandInquisitorId, new List<Role> { Role.Vilian });
            SeedEmployee(currentId++, "Thirteenth", "Sister", "GE00000011", grandInquisitorId, new List<Role> { Role.Vilian });

            //TMNT
            var splinterId = currentId++;
            SeedEmployee(splinterId, "Splinter", "Rat", "TMNT000001", null, new List<Role> { Role.Hero, Role.Leader });
            SeedEmployee(currentId++, "Leonardo", "Turtle", "TMNT000002", splinterId, new List<Role> { Role.Hero, Role.Leader });
            SeedEmployee(currentId++, "Raphael", "Turtle", "TMNT000003", splinterId, new List<Role> { Role.Hero });
            SeedEmployee(currentId++, "Donatello", "Turtle", "TMNT000004", splinterId, new List<Role> { Role.Hero });
            SeedEmployee(currentId++, "Michelangelo", "Turtle", "TMNT000005", splinterId, new List<Role> { Role.Hero });
            SeedEmployee(currentId++, "April", "O'Niel", "TMNT000006", null, new List<Role> { Role.Hero });
            SeedEmployee(currentId++, "Casey", "Jones", "TMNT000007", null, new List<Role> { Role.Hero });

            void SeedEmployee(int id, string firstName, string lastName, string employeeId, int? managerId, IEnumerable<Role> roles)
            {
                migrationBuilder.InsertData(
                    table: nameof(SSquaredDbContext.Employees),
                    columns: new string[] { nameof(Employee.Id), nameof(Employee.FirstName), nameof(Employee.LastName), nameof(Employee.EmployeeId), nameof(Employee.ManagerId) },
                    values: new string[] { id.ToString(), firstName, lastName, employeeId, managerId?.ToString() });

                foreach (var role in roles)
                {
                    migrationBuilder.InsertData(
                        table: nameof(SSquaredDbContext.EmployeeRoles),
                        columns: new string[] { nameof(EmployeeRole.EmployeeId), nameof(EmployeeRole.RoleId) },
                        values: new string[] { id.ToString(), role.Id.ToString() });
                }
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
