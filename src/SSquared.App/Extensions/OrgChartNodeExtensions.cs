using SSquared.App.DTO;
using SSquared.Lib.OrgChart;

namespace SSquared.App.Extensions
{
    public static class OrgChartNodeExtensions
    {
        public static OrgChartNodeDto ToOrgChartNodeDto(this OrgChartNode node)
        {
            return new OrgChartNodeDto(
                Id: node.Id,
                FirstName: node.FirstName,
                LastName: node.LastName,
                Nodes: node.Nodes.Select(n => n.ToOrgChartNodeDto()).ToList());
        }
    }
}
