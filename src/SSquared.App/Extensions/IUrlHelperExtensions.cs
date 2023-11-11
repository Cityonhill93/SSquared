using Microsoft.AspNetCore.Mvc;
using SSquared.App.Controllers;

namespace SSquared.App.Extensions
{
    public static class IUrlHelperExtensions
    {
        private const string CONTROLLER_SUFFIX = "Controller";

        public static Uri API_AddEmployee(this IUrlHelper urlHelper)
        {
            var strUrl = urlHelper.Action(
                action: nameof(EmployeeController.AddEmployee),
                controller: nameof(EmployeeController).Replace(CONTROLLER_SUFFIX, string.Empty));

            return new Uri(strUrl!, UriKind.Relative);
        }

        public static Uri API_GetEmployee(this IUrlHelper urlHelper, int id)
        {
            var values = new { id = id };
            var strUrl = urlHelper.Action(
                action: nameof(EmployeeController.GetEmployee),
                controller: nameof(EmployeeController).Replace(CONTROLLER_SUFFIX, string.Empty),
                values: values);

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

        public static Uri API_UpdateEmployee(this IUrlHelper urlHelper, int id)
        {
            var values = new { id = id };
            var strUrl = urlHelper.Action(
                action: nameof(EmployeeController.UpdateEmployee),
                controller: nameof(EmployeeController).Replace(CONTROLLER_SUFFIX, string.Empty),
                values: values);

            return new Uri(strUrl!, UriKind.Relative);
        }

        public static Uri AddEmployee(this IUrlHelper urlHelper)
        {
            var strUrl = urlHelper.Page(
                pageName: "/AddEmployee");

            return new Uri(strUrl!, UriKind.Relative);
        }

        public static Uri OrgChart(this IUrlHelper urlHelper, int id)
        {
            var values = new { id = id, v = (int?)null };   //For some reason, the API versioning wants to put the version number here....set it to null until you figure out why it is doing this
            var strUrl = urlHelper.Page(
                pageName: "/OrgChart",
                values: values);

            return new Uri(strUrl!, UriKind.Relative);
        }

        public static Uri UpdateEmployee(this IUrlHelper urlHelper, int id)
        {
            var values = new { id = id, v = (int?)null };   //For some reason, the API versioning wants to put the version number here....set it to null until you figure out why it is doing this
            var strUrl = urlHelper.Page(
                pageName: "/UpdateEmployee",
                values: values);

            return new Uri(strUrl!, UriKind.Relative);
        }
    }
}

