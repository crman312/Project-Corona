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
    public class UpdateEmployeeModel : PageModel
    {
        private readonly ILogger<UpdateEmployeeModel> _logger;

        public UpdateEmployeeModel(ILogger<UpdateEmployeeModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(EmployeeModel employee)
        {
            
        }

        /*public List<string> ShowEmployees()
        {
            List<string> EmployeeNames = new List<string>();
            var cs = Database.Database.Connector();

            using var con = new NpgsqlConnection(cs);
            con.Open();
            string sql = @"SELECT * from employees";
            using var cmd = new NpgsqlCommand(sql, con);

            using (NpgsqlDataReader getEmployeeInfo = cmd.ExecuteReader())
            {
                DataTable dataTable = new DataTable();

                dataTable.Load(getEmployeeInfo);
                foreach (DataRow row in dataTable.Rows)
                {
                    EmployeeNames.Add(row["name"]);
                }
                con.Close();
                return EmployeeNames;
            }
        }*/
    }
}