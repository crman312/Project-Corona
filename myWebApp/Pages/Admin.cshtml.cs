using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using myWebApp.Database;
using myWebApp.Models;
using Npgsql;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;


namespace myWebApp.Pages
{
    public class AdminModel : PageModel
    {

        private readonly ILogger<AdminModel> _logger;

        public AdminModel(ILogger<AdminModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
        public string Info { get; set; }
        
        public void OnPostSubmitCompanyName(CompanyNameModel company)
        {
            this.Info = string.Format("Successfully wsfs");
            bool check = CheckIfExist();
            int id = 1;
            if(check == false)
            {
                CreateName(company.CompanyName, id);
                this.Info = string.Format("Successfully saved");
            }
            if(check == true)
            {
                UpdateName(company.CompanyName, id);
                this.Info = string.Format("Successfully updated");
            }  
        }

        public void UpdateName(string Company_name, int Id)
        {
            var cs = Database.Database.Connector();

            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "UPDATE setting SET company_name = @Company_name WHERE id = @Id";
            using var cmd = new NpgsqlCommand(sql, con);
            
            cmd.Parameters.Add(new NpgsqlParameter("@company_name", Company_name));
            cmd.Parameters.Add(new NpgsqlParameter("@id", Id));

            cmd.ExecuteNonQuery();
            cmd.Dispose();  

            con.Close();
        }

        public void CreateName(string Company_name, int Id)
        {
            var cs = Database.Database.Connector();

            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "INSERT INTO setting(company_name, id) VALUES(@Company_name, @Id)";
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("company_name", Company_name);
            cmd.Parameters.AddWithValue("id", Id);

            cmd.Prepare();

            cmd.ExecuteNonQuery();
            con.Close();
        }

        public bool CheckIfExist()
        {
            var cs = Database.Database.Connector();

            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "SELECT * FROM setting";
            using var cmd = new NpgsqlCommand(sql, con);

            NpgsqlDataReader dRead = cmd.ExecuteReader();
            
            while (dRead.Read())
            {
                for(int i = 0; i < dRead.FieldCount; i++)
                {
                    if(dRead[1].ToString() != "1")
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public void  OnPostSubmit(NotificationModel notif)
        {
            DateTime datenow = DateTime.Now;
            CreateNotification(datenow, notif.Bericht);
        }


        public void CreateNotification(DateTime convdayid, string Bericht) 
        {
            var cs = Database.Database.Connector();

            using var con = new NpgsqlConnection(cs);
            con.Open();


            var sql = "INSERT INTO notification(bericht, datumnu) VALUES(@Msg, @Date)";
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("Msg", Bericht);
            cmd.Parameters.AddWithValue("Date", convdayid);

            cmd.Prepare();

            cmd.ExecuteNonQuery();
            con.Close(); 
        }
    }
}