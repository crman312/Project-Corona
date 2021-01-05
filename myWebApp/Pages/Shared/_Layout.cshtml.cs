using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using myWebApp.Database;
using myWebApp.Models;
using myWebApp.Pages;
using Npgsql;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;


namespace myWebApp.Pages
{
    public class LayoutModel : PageModel
    {

        private readonly ILogger<LayoutModel> _logger;

        public LayoutModel(ILogger<LayoutModel> logger)
        {
            _logger = logger;
        }

        public string CompanyName {get; set;}

        public void OnGet()
        {
            CompanyName = ChangeNameModel.GetCompanyName();
        }

    }
}