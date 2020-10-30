using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace myWebApp.Pages
{
    public class AddWorkspaceModel : PageModel
    {
        private readonly ILogger<AddWorkspaceModel> _logger;

        public AddWorkspaceModel(ILogger<AddWorkspaceModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}