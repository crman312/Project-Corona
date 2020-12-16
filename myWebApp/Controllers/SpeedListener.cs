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

namespace myWebApp.Controllers
{
    public class SpeedListener :Controller
    {
        private IHubContext<speedalarmhub> _hubContext;
        private IMemoryCache _cache;
        public SpeedListener(IHubContext<speedalarmhub> hubContext,IMemoryCache cache)
        {
            _hubContext = hubContext;
            _cache = cache; 
        }
        public static string  cs = Database.Database.Connector();
        public void ListenForAlarmNotifications()
        {
            NpgsqlConnection conn = new NpgsqlConnection(cs);
            conn.StateChange += conn_StateChange;
            conn.Open();
            var listenCommand = conn.CreateCommand();
            listenCommand.CommandText = $"listen notifytickets;";
            listenCommand.ExecuteNonQuery();
            conn.Notification += PostgresNotificationReceived;
            _hubContext.Clients.All.SendAsync(this.GetAlarmList());
            while (true)
            {
                conn.Wait();
            }
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
            _hubContext.Clients.All.SendAsync("ReceiveMessage", this.GetAlarmList());
        }
        public string GetAlarmList()
        {
            List<string> not = new List<string>();
            using var con = new NpgsqlConnection(cs);
            {
                string query = "Select datumnu, bericht FROM notification ORDER BY datumnu DESC LIMIT 1";
                using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                {
                    cmd.Connection = con;
                    con.Open();
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        
                        
                        while (dr.Read())
                        {
                            string Datenow = ((DateTime) dr["datumnu"]).ToString("yyyy/MM/dd");
                            string Bericht = dr["bericht"].ToString();
                            not.Add( "Date is: " + Datenow + ", Message: " + Bericht);
                        }
                    }
                    
                    con.Close();
                }
            }
            _cache.Set("notification", SerializeObjectToJson(not));
            return _cache.Get("notification").ToString();
        }
        public String SerializeObjectToJson(Object notification)
        {
            try
            {
                
                return  Newtonsoft.Json.JsonConvert.SerializeObject(notification);
            }
            catch (Exception) { return null; }
        }
        private void conn_StateChange(object sender, System.Data.StateChangeEventArgs e)
        {

            _hubContext.Clients.All.SendAsync("Current State: " + e.CurrentState.ToString() + " Original State: " + e.OriginalState.ToString(), "connection state changed");
        }
    }
}