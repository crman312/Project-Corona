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
            var ss = ShowEmployees();
            ViewData["MyNumber"] = 42;
            ViewData["MyString"] = ss;
        }

        public List<string> ShowEmployees()
        {
            List<string> EmployeeNames = new List<string>();

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
                    EmployeeNames.Add(dRead[1].ToString());
                }
            }
            return EmployeeNames;
        }
        
    }
}