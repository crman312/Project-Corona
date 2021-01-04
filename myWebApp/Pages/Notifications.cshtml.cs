using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using myWebApp.Hubs;
using myWebApp.Controllers;
using myWebApp.Database;
using myWebApp.Models;
using myWebApp.Pages;
using Npgsql;

namespace myWebApp.Pages
{
    public class NotificationsModel : PageModel
    {
        private readonly ILogger<NotificationsModel> _logger;
        

        

        public NotificationsModel(ILogger<NotificationsModel> logger)
        {
            _logger = logger;
            
        }


        public List<NotificationModel> ShowNotification()
        {
            

            var cs = Database.Database.Connector();
            List<NotificationModel> not = new List<NotificationModel>();
            using var con = new NpgsqlConnection(cs);
            {
                string query = "Select datumnu, bericht FROM notification ORDER BY datumnu DESC OFFSET 1";
                using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                {
                    cmd.Connection = con;
                
                    con.Open();
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        
                        while (dr.Read())
                        {
                            not.Add(new NotificationModel { Datenow = ((DateTime) dr["datumnu"]).ToString("yyyy/MM/dd"), Bericht = dr["bericht"].ToString() });
                        }
                    }
                    
                    con.Close();
                }
            }
            return not;
        }
        private void PostgresNotificationReceived(object sender, NpgsqlNotificationEventArgs e)
        {

            string actionName = e.Payload.ToString();
            string actionType = "";
            if (actionName.Contains("DELETE"))
            {
                actionType = "Delete";
            }
            if (actionName.Contains("UPDATE"))
            {
                actionType = "Update";
            }
            if (actionName.Contains("INSERT"))
            {
                actionType = "Insert";
            }
            
        }

       
    }
}