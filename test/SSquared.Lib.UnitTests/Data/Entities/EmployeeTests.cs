using SSquared.Lib.Data.Entities;
using Xunit;

namespace SSquared.Lib.UnitTests.Data.Entities
{
    public class EmployeeTests
    {
        [Fact]
        public void MayBeManagedByUnrlatedEmployee()
        {
            var employee = new Employee
            {
                Id = 1,
                FirstName = "Sample",
                LastName = "Employee",
                EmployeeId = "12345"
            };

            var manager = new Employee
            {
                Id = 2,
                FirstName = "Sample",
                LastName = "Manager",
                EmployeeId = "54321"
            };

            var managerIsValid = employee.MayBeManagedBy(manager);
            Assert.True(managerIsValid);
        }

        [Fact]
        public void MayNotBeManagedByEmployee()
        {
            const int employeeId = 1;
            const int managerId = 2;

            var employee = new Employee
            {
                Id = employeeId,
                FirstName = "Sample",
                LastName = "Employee",
                EmployeeId = "12345",
                ManagerId = managerId
            };

            var manager = new Employee
            {
                Id = managerId,
                FirstName = "Sample",
                LastName = "Manager",
                EmployeeId = "54321",
                Employees = new List<Employee> { employee }
            };

            var managerIsValid = manager.MayBeManagedBy(employee);
            Assert.False(managerIsValid);
        }

        [Fact]
        public void MayNotManageSelf()
        {
            var employee = new Employee
            {
                Id = 1,
                FirstName = "Sample",
                LastName = "Employee",
                EmployeeId = "12345"
            };

            var managerIsValid = employee.MayBeManagedBy(employee);
            Assert.False(managerIsValid);
        }
    }
}
