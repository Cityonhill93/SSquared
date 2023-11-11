namespace SSquared.Lib.OrgChart
{
    public record OrgChartNode(int Id, string FirstName, string LastName)
    {
        public IEnumerable<OrgChartNode> Nodes { get; init; } = new List<OrgChartNode>();
    }
}
