using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Project_Corona.Pages
{
    public class testModel : PageModel
    {
        private readonly ILogger<testModel> _logger;

        public testModel(ILogger<testModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}