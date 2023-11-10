using SSquared.App.DTO;
using SSquared.Lib.Data.Entities;

namespace SSquared.App.Extensions
{
    public static class RoleExtensions
    {
        public static RoleDto ToRoleDto(this Role role)
        {
            return new RoleDto(
                Id: role.Id,
                Name: role.Name);
        }
    }
}
