using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using myWebApp.Database;
using myWebApp.Pages;
using myWebApp.Models;
using myWebApp.Controllers;
using Npgsql;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace myWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        [BindProperty]
        public string userEmail {get; set;}

        [BindProperty]
        public string userEmail {get; set;}

        public void OnGet()
        {
        }

        public IActionResult OnPostSubmit(LoginModel login)
        {
            string encryptedpassword = AddEmployeeModel.sha256_hash(login.Password);
            Tuple<bool, int> log = LoginCheck(login.Email, encryptedpassword);
             // User name to pass to the next page in future
        
            if(log.Item1 == true && log.Item2 == 1)
            {
                return new RedirectToPageResult("Admin");
            }
            else if(log.Item1 == true && log.Item2 == 2)
            {
                HttpContext.Session.SetString("useremail", userEmail);
                return new RedirectToPageResult("EmployeeIndex");
            }
            else
            {
                return null;
            }
        }

        public Tuple<bool, int> LoginCheck(string Email, string Password)
        {
            userEmail = Email;
            var cs = Database.Database.Connector();

            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "SELECT * FROM employees";
            using var cmd = new NpgsqlCommand(sql, con);

            NpgsqlDataReader dRead = cmd.ExecuteReader();

            int employeeFunction = 0;
            string User = "";
           
            while (dRead.Read())
            {
                for(int i = 0; i < dRead.FieldCount; i++)
                {
                    if(dRead[1].ToString() == Email && dRead[2].ToString() == Password)
                    {
                        if(dRead[3].ToString() == "admin" || dRead[3].ToString() == "Admin" || dRead[3].ToString() == "ADMIN")
                        {
                            employeeFunction = 1;
                            User = dRead[0].ToString();
                            return Tuple.Create(true, employeeFunction);
                        }
                        User = dRead[0].ToString();
                        return Tuple.Create(true, employeeFunction);
                    }
                }
            }
            return Tuple.Create(false, employeeFunction);
        }
    }
}