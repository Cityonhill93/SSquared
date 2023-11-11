namespace SSquared.App.DTO
{
    public record OrgChartNodeDto(
        int Id,
        string FirstName,
        string LastName,
        IEnumerable<OrgChartNodeDto> Nodes);
}
