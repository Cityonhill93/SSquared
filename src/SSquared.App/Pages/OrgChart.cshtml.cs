using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSquared.Lib.Repositories;

namespace SSquared.App.Pages
{
    public class OrgChartModel : PageModel
    {
        public OrgChartModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        private readonly IEmployeeRepository _employeeRepository;

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var employee = await _employeeRepository.GetAsync(Id, HttpContext.RequestAborted);
            if (employee is null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
