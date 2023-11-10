using Microsoft.AspNetCore.Mvc;
using SSquared.App.Controllers;

namespace SSquared.App.Extensions
{
    public static class IUrlHelperExtensions
    {
        private const string CONTROLLER_SUFFIX = "Controller";

        public static Uri API_GetEmployee(this IUrlHelper urlHelper, int id)
        {
            var strUrl = urlHelper.Action(
                action: nameof(EmployeeController.GetEmployee),
                controller: nameof(EmployeeController).Replace(CONTROLLER_SUFFIX, string.Empty));

            return new Uri(strUrl!, UriKind.Relative);
        }

        public static Uri API_GetEmployees(this IUrlHelper urlHelper, string? filter = null)
        {
            var values = new { filter = filter };
            var strUrl = urlHelper.Action(
                action: nameof(EmployeeController.GetEmployee),
                controller: nameof(EmployeeController).Replace(CONTROLLER_SUFFIX, string.Empty),
                values: values);

            return new Uri(strUrl!, UriKind.Relative);
        }
    }
}

