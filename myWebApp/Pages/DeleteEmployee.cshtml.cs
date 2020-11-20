using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
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

        public string Info { get; set; }

        public void OnGet(){
            
            var cs = Database.Database.Connector();
			using var con = new NpgsqlConnection(cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;

            cmd.CommandText = @"SELECT * FROM employees ";

            using var getemployees = cmd.ExecuteReader();
            
            DataTable dataTable = new DataTable();
            dataTable.Load(getemployees);
            foreach(DataRow row in dataTable){
                ViewData["employeeName"] = row["Name"];
            }
        }

        public void OnPostSubmit(EmployeeModel employee){
            DeleteEmployee(employee.Name);
            this.Info = string.Format("Succesfully deleted employee {0}", employee.Name);
        }

        public void DeleteEmployee(string Name){
            var cs = Database.Database.Connector();
			using var con = new NpgsqlConnection(cs);
            con.Open();

            using var cmd = new NpgsqlCommand($"DELETE FROM employees WHERE name = {Name}", con);
            cmd.ExecuteNonQuery();
        }
    }
}