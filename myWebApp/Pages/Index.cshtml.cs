using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Npgsql;


namespace myWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        
    protected void LoginButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("/admin");

    }
        public void LoginCheck(string email, string password)
        {
            try
			{
                

                string emailInput = Request.Form["email"];
                string passwordInput = Request.Form["password"];



                

            }
            catch (Exception)
			{

				throw;
			}
			
        }
    }
}