using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Project_Corona.Database;
using Project_Corona.Models;
using Npgsql;


namespace Project_Corona.Pages
{
    public class EmployeepageModel : PageModel
    {
        private readonly ILogger<EmployeepageModel> _logger;
        
        public EmployeepageModel(ILogger<EmployeepageModel> logger)
        {
            _logger = logger;
        }

        public string Info { get; set; }
        
        

        public void OnGet()
        {
            
        
        }
        public void  OnPostSubmit(ReservationModel reservation)
        {
            this.Info = string.Format("Reservation successfully saved");
            
            DateTime convdayid = Convert.ToDateTime(reservation.Dayid);
            CreateReservation(convdayid, reservation.Roomid, reservation.Email, reservation.Locationid);
        }   

        public void CreateReservation(DateTime convdayid, string Roomid, string Email, string Locationid) 
        {

            
            var cs = Database.Database.Connector();

            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "INSERT INTO reservation(dayid, roomid, email, locationid) VALUES(@Dayid, @Roomid, @Email, @Locationid)";
            using var cmd = new NpgsqlCommand(sql, con);
            
            cmd.Parameters.AddWithValue("dayid", convdayid);
            cmd.Parameters.AddWithValue("roomid", Roomid);
            cmd.Parameters.AddWithValue("email", Email);
            cmd.Parameters.AddWithValue("locationid", Locationid);

            cmd.Prepare();

            cmd.ExecuteNonQuery();
            con.Close();  
        }

        public List<WorkspaceModel> PopulateReservations()
        {
            var cs = Database.Database.Connector();
            List<WorkspaceModel> res = new List<WorkspaceModel>();
            using var con = new NpgsqlConnection(cs);
            {
                string query = "Select location FROM workspaces";
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


        public List<ReservationModel> ShowReservation()
        {
            var cs = Database.Database.Connector();
            List<ReservationModel> res = new List<ReservationModel>();
            using var con = new NpgsqlConnection(cs);
            {
                string query = "Select dayid, locationid FROM reservation";
                using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                {
                    cmd.Connection = con;
                    con.Open();
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        
                        while (dr.Read())
                        {
                            res.Add(new ReservationModel { Dayid = ((DateTime) dr["dayid"]).ToString("MM/dd/yyyy"), Locationid = dr["locationid"].ToString() });
                        }
                    }
                    
                    con.Close();
                }
            }


            return res;
        }



    }
}
        // Create
        

        

    
