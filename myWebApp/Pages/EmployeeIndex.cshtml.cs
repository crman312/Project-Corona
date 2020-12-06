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
using myWebApp.Database;
using myWebApp.Models;
using myWebApp.Pages;
using Npgsql;


namespace myWebApp.Pages
{
    public class EmployeeIndexModel : PageModel
    {
        private readonly ILogger<EmployeeIndexModel> _logger;
        
        public EmployeeIndexModel(ILogger<EmployeeIndexModel> logger)
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
            
            DateTime convdayid = Convert.ToDateTime(reservation.Date);
            if (CheckReservation(convdayid, userEmail) == true) {

                CreateReservation(convdayid, reservation.Room, userEmail, reservation.Location);
                this.Info = string.Format("Reservation successfully saved");
            }
            else if (CheckReservation(convdayid, userEmail) == false) {
                this.Info = string.Format("You entered same date, or reserve before today, try different date");
            }
        }   

        public void OnPostRemove(ReservationModel reservation)
        {
            userEmail = HttpContext.Session.GetString("useremail");
            
            DateTime convdayid = Convert.ToDateTime(reservation.Date);
            DeleteReservation(convdayid, reservation.Location);
        }

        public void DeleteReservation(DateTime convdayid, string Location)
        {
            
            var cs = Database.Database.Connector();
            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "DELETE FROM reservation WHERE location = @Location AND date = @Date";
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("Location", Location);
            cmd.Parameters.AddWithValue("Date", convdayid);
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
                string query = "Select date FROM reservation WHERE email = '"+ Email+"'";
                using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                {
                    cmd.Connection = con;
                    con.Open();
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            res.Add(((DateTime) dr["date"]));
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


        public void CreateReservation(DateTime convdayid, string Roomid, string Email, string Location) 
        {

            
            var cs = Database.Database.Connector();

            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "INSERT INTO reservation(date, roomid, email, location) VALUES(@Date, @Roomid, @Email, @Location)";
            using var cmd = new NpgsqlCommand(sql, con);
            
            cmd.Parameters.AddWithValue("date", convdayid);
            cmd.Parameters.AddWithValue("roomid", Roomid);
            cmd.Parameters.AddWithValue("email", Email);
            cmd.Parameters.AddWithValue("location", Location);

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
                string query = "Select date, location FROM reservation WHERE email = '"+ userEmail+"'";
                using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                {
                    cmd.Connection = con;
                    con.Open();
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        
                        while (dr.Read())
                        {
                            res.Add(new ReservationModel { Date = ((DateTime) dr["date"]).ToString("yyyy/MM/dd"), Location = dr["location"].ToString() });
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
        
