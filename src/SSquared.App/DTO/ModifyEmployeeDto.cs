namespace SSquared.App.DTO
{
    public record ModifyEmployeeDto(
        string FirstName,
        string LastName,
        string EmployeeId)
    {
        public int? ManagerId { get; init; }

        public IEnumerable<int> RoleIds { get; init; } = new List<int>();

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(FirstName)
                && !string.IsNullOrWhiteSpace(LastName)
                && !string.IsNullOrWhiteSpace(EmployeeId);
        }
    }
}
