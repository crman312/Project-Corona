using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using myWebApp.Database;
using myWebApp.Models;
using myWebApp.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Npgsql;

namespace myWebApp.Pages
{
    public class AddReservationModel : PageModel
    {
        private readonly ILogger<AddReservationModel> _logger;

        public AddReservationModel(ILogger<AddReservationModel> logger)
        {
            _logger = logger;
        }

        public string Info { get; set; }
        public string userEmail { get; set; }


        public void OnGet()
        {
        }


        public void OnPostSubmit(ReservationModel reservation)
        {
            DateTime convdayid = Convert.ToDateTime(reservation.Date);
            
            bool check = prioCheck(reservation);
            bool check1 = CheckReservation(convdayid, userEmail);
            if(check && check1){
              CreateReservation(reservation.Email, convdayid, reservation.Location, reservation.Room);
              this.Info = string.Format("Sucessfully added the reservation");
            }
            else{
              if (check1 == false) {
              this.Info = string.Format("You entered same date, or tried to reserve in the past, try different date");
              }
              if(check == false)
              { 
                this.Info = string.Format("You do not have the right priority, please try a later date");
              }
            }
        }

        public bool prioCheck(ReservationModel reservation)
        {
          DateTime convdayid = Convert.ToDateTime(reservation.Date);
          userEmail = HttpContext.Session.GetString("useremail");
          
          var cs = Database.Database.Connector(); // start connectie met database

          List<string> pr = new List<string>(); //list creeren

          using var con = new NpgsqlConnection(cs);
          {
            string query = "Select priority FROM employees WHERE email = '"+ userEmail+"'";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            {
              cmd.Connection = con;
              con.Open();
              using (NpgsqlDataReader dr = cmd.ExecuteReader())
              {
                while (dr.Read())
                {
                  pr.Add(((string) dr["priority"]));
                }
              }
              con.Close(); //sluit de connection, maar de list pr bestaat nog
            }
          }

          foreach(string priority in pr)
          {
            if(priority == "Low") // 2 dagen van te voren
            {
              DateTime newdt = convdayid.AddDays(-2);
              if(newdt > DateTime.Now){return true;}
              else{return false;}
            }
            else if(priority == "Medium") // 7 dagen van te voren
            {
              DateTime newdt = convdayid.AddDays(-7);
              if(newdt > DateTime.Now){return true;}
              else{return false;}
            }
            else // high priority kan altijd reserveren
            {
              return true;
            }
          }
          return false;
        }

        public void CreateReservation(string Email, DateTime Date, string Location, string Room)
        {
            var cs = Database.Database.Connector();

            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "INSERT INTO reservations(res_email, date, res_location, res_room) VALUES(@email, @date, @location, @room)";
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("email", Email);
            cmd.Parameters.AddWithValue("date", Date);
            cmd.Parameters.AddWithValue("location", Location);
            cmd.Parameters.AddWithValue("room", Room);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
            con.Close();   
        }  

        public List<Reservations> ShowReservations()
        {
            List<Reservations> Reservations = new List<Reservations>();

            var cs = Database.Database.Connector();

            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "SELECT res_email, date, res_location, res_room FROM reservations ORDER BY date ASC, res_location ASC";
            using var cmd = new NpgsqlCommand(sql, con);

            NpgsqlDataReader dRead = cmd.ExecuteReader();
           
            while (dRead.Read())
            {
                Reservations.Add(new Reservations(dRead[0].ToString(),dRead[1].ToString(),dRead[2].ToString(),dRead[3].ToString()));
            }
            return Reservations;
        }
        public bool CheckReservation(DateTime convdayid, string Email) 
        {   
            int AmountDate = 0;
            DateTime now = DateTime.Now;
           
            var cs = Database.Database.Connector();
            List<DateTime> res = new List<DateTime>();
            using var con = new NpgsqlConnection(cs);
            {
                string query = "Select date FROM reservations WHERE res_email = '"+ Email+"'";
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

        
    }

  public class Reservations{
      public Reservations(string email, string date, string location, string room){
          Email = email;
          Date = date;
          Location = location;
          Room = room;
      }
      public string Email {get; set;}
      public string Date {get; set;}
      public string Location {get; set;}
      public string Room { get; set; }

    }
}
