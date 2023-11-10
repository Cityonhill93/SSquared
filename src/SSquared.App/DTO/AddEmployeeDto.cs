namespace SSquared.App.DTO
{
    public record AddEmployeeDto(
        string FirstName,
        string LastName,
        string EmployeeId)
    {
        public int? ManagerId { get; init; }

        public IEnumerable<int> RoleIds { get; init; } = new List<int>();
    }
}
