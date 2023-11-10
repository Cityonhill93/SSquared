using Microsoft.AspNetCore.Mvc;
using SSquared.App.DTO;
using SSquared.Lib.Data.Entities;

namespace SSquared.App.Extensions
{
    public static class EmployeeExtensions
    {
        public static EmployeeDto ToEmployeeDto(this Employee employee, IUrlHelper urlHelper) => new EmployeeDto(
            Id: employee.Id,
            FirstName: employee.FirstName,
            LastName: employee.LastName,
            EmployeeId: employee.EmployeeId,
            GetUrl: urlHelper.API_GetEmployee(employee.Id));

        public static ExpandedEmployeeDto ToExpandedEmployeeDto(this Employee employee, IUrlHelper urlHelper) => new ExpandedEmployeeDto(
            id: employee.Id,
            firstName: employee.FirstName,
            lastName: employee.LastName,
            employeeId: employee.EmployeeId,
            manager: employee.Manager?.ToEmployeeDto(urlHelper),
            employees: employee.Employees.Select(e => e.ToEmployeeDto(urlHelper)),
            getUrl: urlHelper.API_GetEmployee(employee.Id));
    }
}
