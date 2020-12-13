using System;
using Microsoft.AspNetCore.SignalR;
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
        [BindProperty]
        public string userEmail {get; set;}

        

        public void OnGet()
        {
        }

        


        public IActionResult OnPostSubmit(EmployeeModel login)
        {
            string encryptedpassword = AddEmployeeModel.sha256_hash(login.Password);
            Tuple<bool, int> log = LoginCheck(login.Email, login.Password);
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
            
           
            while (dRead.Read())
            {
                for(int i = 0; i < dRead.FieldCount; i++)
                {
                    if(dRead["email"].ToString() == userEmail && dRead["password"].ToString() == Password)
                    {
                        if(dRead["function"].ToString() == "admin" || dRead["function"].ToString() == "Admin" || dRead["function"].ToString() == "ADMIN")
                        {
                            employeeFunction = 1;
                            return Tuple.Create(true, employeeFunction);
                        }
                        employeeFunction = 2;
                        return Tuple.Create(true, employeeFunction);
                    }
                }
            }
            return Tuple.Create(false, employeeFunction);
        }
    }
}