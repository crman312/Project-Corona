using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Project_Corona.Database;
using Project_Corona.Models;
using Project_Corona.Pages;
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
        public string userEmail {get; set;}

        
        public void OnGet()
        {
            userEmail = HttpContext.Session.GetString("useremail");
            
        
        }
        public void  OnPostSubmit(ReservationModel reservation)
        {
            userEmail = HttpContext.Session.GetString("useremail");
            
            DateTime convdayid = Convert.ToDateTime(reservation.Dayid);
            if (CheckReservation(convdayid, userEmail) == true) {

                CreateReservation(convdayid, reservation.Roomid, userEmail, reservation.Locationid);
                this.Info = string.Format("Reservation successfully saved");
            }
            else if (CheckReservation(convdayid, userEmail) == false) {
                this.Info = string.Format("You entered same date, or reserve before today, try different date");
            }
        }   

        public void OnPostRemove(ReservationModel reservation)
        {
            userEmail = HttpContext.Session.GetString("useremail");
            
            DateTime convdayid = Convert.ToDateTime(reservation.Dayid);
            DeleteReservation(convdayid, reservation.Locationid);
        }

        public void DeleteReservation(DateTime convdayid, string Locationid)
        {
            
            var cs = Database.Database.Connector();
            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "DELETE FROM reservation WHERE locationid = @Locationid AND dayid = @Dayid";
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("Locationid", Locationid);
            cmd.Parameters.AddWithValue("Dayid", convdayid);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public bool CheckReservation(DateTime convdayid, string Email) 
        {   
            int AmountDate = 0;
            DateTime now = DateTime.Now;
           
            var cs = Database.Database.Connector();
            List<DateTime> res = new List<DateTime>();
            using var con = new NpgsqlConnection(cs);
            {
                string query = "Select dayid FROM reservation WHERE email = '"+ Email+"'";
                using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                {
                    cmd.Connection = con;
                    con.Open();
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            res.Add(((DateTime) dr["dayid"]));
                        }
                        
                    }
                    
                    
                }
            }
            foreach(DateTime p in res)
            {
                if (p == convdayid || p < now)
                {
                    AmountDate++;
                    
                }
                
            }
            if (AmountDate >= 1)
            {
                return false;
            }
            else{
                return true;
            }
            


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
                string query = "Select dayid, locationid FROM reservation WHERE email = '"+ userEmail+"'";
                using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                {
                    cmd.Connection = con;
                    con.Open();
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        
                        while (dr.Read())
                        {
                            res.Add(new ReservationModel { Dayid = ((DateTime) dr["dayid"]).ToString("yyyy/MM/dd"), Locationid = dr["locationid"].ToString() });
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
        

        

    
