using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using myWebApp.Database;
using myWebApp.Models;
using Npgsql;


namespace myWebApp.Pages
{
    public class DeleteEmployeeModel : PageModel
    {
        private readonly ILogger<DeleteEmployeeModel> _logger;

        public DeleteEmployeeModel(ILogger<DeleteEmployeeModel> logger)
        {
            _logger = logger;
        }
        
        public void OnGet()
        {
            ViewData["MyNumber"] = 42;
            ViewData["MyString"] = "Hello World";
        }
    }
}