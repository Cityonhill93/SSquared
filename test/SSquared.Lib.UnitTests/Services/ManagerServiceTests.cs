using Moq;
using SSquared.Lib.Data.Entities;
using SSquared.Lib.Repositories;
using SSquared.Lib.Services;
using Xunit;

namespace SSquared.Lib.UnitTests.Services
{
    public class ManagerServiceTests
    {
        [Fact]
        public async Task MayBeManagedByUnrlatedEmployee()
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

            var mockRepo = new Mock<IEmployeeRepository>();
            mockRepo
                .Setup(r => r.GetByManagerId(employee.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Employee>());

            var managerService = new ManagerService(mockRepo.Object);
            var managerIsValid = await managerService.MayBeManagedByAsync(employee, manager);
            Assert.True(managerIsValid);
        }

        [Fact]
        public async Task MayNotBeManagedByEmployee()
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

            var mockRepo = new Mock<IEmployeeRepository>();
            mockRepo
                .Setup(r => r.GetByManagerId(employee.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Employee>()
                {
                    manager
                });

            var managerService = new ManagerService(mockRepo.Object);
            var managerIsValid = await managerService.MayBeManagedByAsync(employee, manager);
            Assert.False(managerIsValid);
        }

        [Fact]
        public async Task MayNotBeManagedByIndirectEmployee()
        {
            var employee = new Employee
            {
                Id = 1,
                FirstName = "Sample",
                LastName = "Employee",
                EmployeeId = "12345"
            };

            var directEmployee = new Employee
            {
                Id = 2,
                FirstName = "Direct",
                LastName = "Employee",
                EmployeeId = "54321"
            };

            var indirectEmployee = new Employee
            {
                Id = 3,
                FirstName = "Indirect",
                LastName = "Employee",
                EmployeeId = "54322"
            };

            var mockRepo = new Mock<IEmployeeRepository>();
            mockRepo
                .Setup(r => r.GetByManagerId(employee.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Employee>()
                {
                    directEmployee
                });
            mockRepo
               .Setup(r => r.GetByManagerId(directEmployee.Id, It.IsAny<CancellationToken>()))
               .ReturnsAsync(new List<Employee>()
               {
                    indirectEmployee
               });

            var managerService = new ManagerService(mockRepo.Object);
            var managerIsValid = await managerService.MayBeManagedByAsync(employee, indirectEmployee);
            Assert.False(managerIsValid);
        }

        [Fact]
        public async Task MayNotManageSelf()
        {
            var employee = new Employee
            {
                Id = 1,
                FirstName = "Sample",
                LastName = "Employee",
                EmployeeId = "12345"
            };

            var managerService = new ManagerService(Mock.Of<IEmployeeRepository>());
            var managerIsValid = await managerService.MayBeManagedByAsync(employee, employee);
            Assert.False(managerIsValid);
        }
    }
}
