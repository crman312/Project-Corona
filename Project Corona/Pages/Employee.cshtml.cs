using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Project_Corona.Pages
{
    public class EmployeeModel : PageModel
    {
        private readonly ILogger<EmployeeModel> _logger;
        
        public EmployeeModel(ILogger<EmployeeModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
        // Create
        
    }
}