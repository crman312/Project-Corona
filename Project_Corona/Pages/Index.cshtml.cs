using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Project_Corona.Database;
using Project_Corona.Models;
using Npgsql;

namespace Project_Corona.Pages
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
        
        public IActionResult OnPostSubmit(EmployeeModel login)
        {
            
            
            string encryptedpassword = AddEmployeeModel.sha256_hash(login.Password);
            Tuple<bool, int> log = LoginCheck(login.Email, login.Password);

            if(log.Item1 == true && log.Item2 == 1)
            {
                return new RedirectToPageResult("Admin");
            }
            else if(log.Item1 == true && log.Item2 == 2)
            {
                return new RedirectToPageResult("Employeepage");
            }
            else
            {
                return null;
            }
        }

        public Tuple<bool, int> LoginCheck(string Email, string Password)
        {
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
                    if(dRead["email"].ToString() == Email && dRead["password"].ToString() == Password)
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
