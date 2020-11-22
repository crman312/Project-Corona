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
        
        public void OnGet(){}

        public List<Employee> ShowEmployees()
        {
            List<Employee> EmployeeNames = new List<Employee>();

            var cs = Database.Database.Connector();

            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "SELECT name, email, function FROM employees";
            using var cmd = new NpgsqlCommand(sql, con);

            NpgsqlDataReader dRead = cmd.ExecuteReader();
           
            while (dRead.Read())
            {
                EmployeeNames.Add(new Employee(dRead[0].ToString(),dRead[1].ToString(),dRead[2].ToString()));
            }
            return EmployeeNames;
        }

        public void OnPostSubmit(EmployeeModel employee){
            DeleteEmployee(employee.Name);
        }

        public void DeleteEmployee(string Name){
            var cs = Database.Database.Connector();
			using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "DELETE FROM employees WHERE name = @Name;";
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("Name", Name);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }

    public class Employee{
        public Employee(string name, string email, string function){
            EmpName = name;
            EmpEmail = email;
            EmpFunction= function;
        }
        public string EmpName {get; set;}
        public string EmpEmail {get; set;}
        public string EmpFunction {get; set;}
    }
}