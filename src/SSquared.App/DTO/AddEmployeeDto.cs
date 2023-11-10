namespace SSquared.App.DTO
{
    public record AddEmployeeDto(
        string FirstName,
        string LastName,
        string EmployeeId)
    {
        public int? ManagerId { get; }
    }
}
