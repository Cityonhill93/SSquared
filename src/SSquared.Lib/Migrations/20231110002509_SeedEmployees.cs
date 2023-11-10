using Microsoft.EntityFrameworkCore.Migrations;
using SSquared.Lib.Data;
using SSquared.Lib.Data.Entities;

#nullable disable

namespace SSquared.Lib.Migrations
{
    /// <inheritdoc />
    public partial class SeedEmployees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string tableName = nameof(SSquaredDbContextDbContext.Employees);
            string[] columns = { nameof(Employee.Id), nameof(Employee.FirstName), nameof(Employee.LastName), nameof(Employee.EmployeeId), nameof(Employee.ManagerId) };

            var currentId = 1;
            var bruceId = currentId++;
            migrationBuilder.InsertData(
                table: tableName,
                columns: columns,
                GetValues(bruceId, "Bruce", "Wayne", "BM00000001", null));

            migrationBuilder.InsertData(
                table: tableName,
                columns: columns,
                GetValues(currentId++, "Robert", "Grayson", "BM00000001", bruceId));

            migrationBuilder.InsertData(
               table: tableName,
               columns: columns,
               GetValues(currentId++, "Robert", "Grayson", "NW00000002", bruceId));

            migrationBuilder.InsertData(
               table: tableName,
               columns: columns,
               GetValues(currentId++, "Barbra", "Gordon", "BG00000001", bruceId));

            var commishId = currentId++;
            migrationBuilder.InsertData(
               table: tableName,
               columns: columns,
               GetValues(commishId, "James", "Gordon", "GPD0000001", null));

            migrationBuilder.InsertData(
               table: tableName,
               columns: columns,
               GetValues(currentId++, "Arnold", "Flass", "GPD0000002", commishId));

            migrationBuilder.InsertData(
               table: tableName,
               columns: columns,
               GetValues(currentId++, "Sarah", "Essen", "GPD0000003", commishId));

            var palpsId = currentId++;
            migrationBuilder.InsertData(
              table: tableName,
              columns: columns,
              GetValues(palpsId, "Sheev", "Palpatine", "GE00000001", null));

            var anakinId = currentId++;
            migrationBuilder.InsertData(
             table: tableName,
             columns: columns,
             GetValues(anakinId, "Anakin", "Skywalker", "GE00000002", palpsId));

            migrationBuilder.InsertData(
             table: tableName,
             columns: columns,
             GetValues(currentId++, "Mas", "Amedda", "GE00000003", palpsId));

            var starkillerId = currentId++;
            migrationBuilder.InsertData(
            table: tableName,
            columns: columns,
            GetValues(starkillerId, "Galen", "Merek", "GE00000004", anakinId));

            migrationBuilder.InsertData(
            table: tableName,
            columns: columns,
            GetValues(currentId++, "Juno", "Eclipse", "GE00000005", starkillerId));

            migrationBuilder.InsertData(
             table: tableName,
             columns: columns,
             GetValues(currentId++, "Mara", "Jae", "GE00000006", palpsId));

            string[] GetValues(int id, string firstName, string lastName, string employeeId, int? managerId)
            {
                string[] values ={
                    id.ToString(),
                    firstName,
                    lastName,
                    employeeId,
                    managerId?.ToString()
                };

                return values;
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
