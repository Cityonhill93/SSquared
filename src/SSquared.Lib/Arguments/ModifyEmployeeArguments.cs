namespace SSquared.Lib.Arguments
{
    public record ModifyEmployeeArguments(
        string FirstName,
        string LastName,
        string EmployeeId,
        int? ManagerId,
        IEnumerable<int> RoleIds);
}
