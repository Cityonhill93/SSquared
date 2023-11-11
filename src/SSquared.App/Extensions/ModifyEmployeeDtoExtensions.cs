using SSquared.App.DTO;
using SSquared.Lib.Arguments;

namespace SSquared.App.Extensions
{
    public static class ModifyEmployeeDtoExtensions
    {
        public static ModifyEmployeeArguments ToModifyEmployeeArguments(this ModifyEmployeeDto dto)
        {
            return new ModifyEmployeeArguments(
                FirstName: dto.FirstName,
                LastName: dto.LastName,
                EmployeeId: dto.EmployeeId,
                ManagerId: dto.ManagerId,
                RoleIds: dto.RoleIds.Distinct().ToList());
        }
    }
}
