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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace myWebApp.Pages
{
    public class MessageModel : PageModel
    {

        private readonly ILogger<MessageModel> _logger;
        
        public MessageModel(ILogger<MessageModel> logger)
        {
            _logger = logger;
        }
        [BindProperty]
        public List<WorkspaceModel> locations {get; set;}
        [BindProperty]
        public List<WorkspaceModel> rooms {get; set;}

        public void OnGet()
        {
            locations = PopulateReservations();
            rooms = ShowRoom();

        }

        public IActionResult OnPostShowRoom(string loc)
        {
            List<WorkspaceModel> l= new List<WorkspaceModel>();
            var cs = Database.Database.Connector();
            using var con = new NpgsqlConnection(cs);
            {
                string query = "Select room FROM workspaces WHERE location = '"+ loc +"'";
                using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                {
                    cmd.Connection = con;
                    con.Open();
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            l.Add(new WorkspaceModel { RoomName = dr["room"].ToString() });
                        }
                    }
                    
                    con.Close();
                }
            }





            return new JsonResult(l);

        }   

        
        public List<WorkspaceModel> PopulateReservations()
        {
            var cs = Database.Database.Connector();
            List<WorkspaceModel> res = new List<WorkspaceModel>();
            using var con = new NpgsqlConnection(cs);
            {
                string query = "Select DISTINCT location FROM workspaces";
                using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                {
                    cmd.Connection = con;
                    con.Open();
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            res.Add(new WorkspaceModel { LocationName = dr["location"].ToString() });
                        }
                    }
                    
                    con.Close();
                }
            }


            return res;
        }

        public List<WorkspaceModel> ShowRoom()
        {
            var cs = Database.Database.Connector();
            List<WorkspaceModel> res = new List<WorkspaceModel>();
            using var con = new NpgsqlConnection(cs);
            {
                string query = "Select room FROM workspaces";
                using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                {
                    cmd.Connection = con;
                    con.Open();
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            res.Add(new WorkspaceModel { RoomName = dr["room"].ToString() });
                        }
                    }
                    
                    con.Close();
                }
            }


            return res;
        }


        public void  OnPostSubmit(ReservationModel res)
        {
            DateTime datenow = DateTime.Now;
            CreateNotification(datenow, res.Location, res.Room);
        
        }

        public void OnPostSendmessg(NotificationModel not)
        {
            DateTime datenow = DateTime.Now;
            CreateNotification2(datenow, not.Bericht);

        }

        public void CreateNotification2(DateTime convdayid, string Bericht) 
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


        public void CreateNotification(DateTime convdayid, string location, string room) 
        {

            string Bericht = "There is a new COVID-19 case in: " + location + " , room " + room;
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
