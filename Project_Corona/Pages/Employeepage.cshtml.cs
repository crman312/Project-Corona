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

            CreateReservation(reservation.Reservationid, reservation.Dayid, reservation.Roomid, reservation.Email, reservation.Locationid);
        }   

        public void CreateReservation(int Reservationid, DateTime Dayid, string Roomid, string Email, string Locationid) 
        {
            var cs = Database.Database.Connector();

            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "INSERT INTO reservation(reservationid, dayid, roomid, email, locationid) VALUES(@Reservationid, @Dayid, @Roomid, @Email, @Locationid)";
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("reservationid", Reservationid);
            cmd.Parameters.AddWithValue("dayid", Dayid);
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


    }
}
        // Create
        

        

    
