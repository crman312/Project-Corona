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


            var sql = "SELECT name, email, function, priority FROM employees ORDER BY priority ASC";

            using var cmd = new NpgsqlCommand(sql, con);

            NpgsqlDataReader dRead = cmd.ExecuteReader();
           
            while (dRead.Read())
            {

                EmployeeNames.Add(new Employee(dRead[0].ToString(),dRead[1].ToString(),dRead[2].ToString(),dRead[3].ToString()));

            }
            return EmployeeNames;
        }

        public void OnPostSubmit(EmployeeModel employee){
            DeleteEmployee(employee.Email);
        }

        public void DeleteEmployee(string Email){
            var cs = Database.Database.Connector();
			using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "DELETE FROM employees WHERE email = @Email;";
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("Email", Email);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }

    public class Employee{

        public Employee(string name, string email, string function, string priority){
            EmpName = name;
            EmpEmail = email;
            EmpFunction = function;
            EmpPriority = priority;

        }
        public string EmpName {get; set;}
        public string EmpEmail {get; set;}
        public string EmpFunction {get; set;}
        public string EmpPriority {get; set;}
    }
}



