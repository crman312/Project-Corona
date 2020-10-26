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
        // Validate the user against the Membership framework user store
        if (Membership.ValidateUser(Username.Value, Password.Value))
        {
            // Log the user into the site
            FormsAuthentication.RedirectFromLoginPage(UserName.Value);
        }
        // If we reach here, the user's credentials were invalid
        InvalidCredentialsMessage.Visible = true;
    }
        public void LoginCheck(string email, string password)
        {
            try
			{
                var cs = "Host=localhost;Username=postgres;Password=admin;Database=Corona kantoor app";
				using var con = new NpgsqlConnection(cs);
                con.Open();
		
                using var cmd = new NpgsqlCommand();
                cmd.Connection = con;

                cmd.CommandText = @"SELECT * FROM employee WHERE email = @email AND password = @password";

                string emailInput = email-text.Value;
                string passwordInput = password-text.Value;



                

            }
            catch (Exception)
			{

				throw;
			}
			
        }
    }
}