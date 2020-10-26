using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace myWebApp.Pages
{
    public class UserModel : PageModel
    {
        private readonly ILogger<AdminModel> _logger;

        public UserModel(ILogger<AdminModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}