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

namespace myWebApp.Pages
{
    public class AdminModel : PageModel
    {
        private readonly ILogger<AdminModel> _logger;

        public void OnGet()
        {
        }

        public AdminModel(ILogger<AdminModel> logger)
        {
            _logger = logger;
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
