using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using myWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace myWebApp.Pages
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