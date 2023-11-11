namespace SSquared.App.DTO
{
    public record EmployeeDto(
        int Id,
        string FirstName,
        string LastName,
        string EmployeeId,
        Uri GetUrl,
        Uri UpdateUri);
}
