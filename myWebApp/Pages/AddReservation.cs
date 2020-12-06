using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using myWebApp.Database;
using myWebApp.Models;
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

        public void OnGet()
        {
        }


        public void OnPostSubmit(ReservationModel reservation)
        {
          bool check = prioCheck(reservation);

            if(check == false)
            { 
              this.Info = string.Format("You do not have the right priority, please try again");
            }

            else
            {
              CreateReservation(reservation.Email, reservation.Date, reservation.Location, reservation.Room);
              this.Info = string.Format("Sucessfully added the reservation");
            }
        }

        public bool prioCheck(ReservationModel reservation)
        {
         string priority = //functie om priority te pakken uit ergens
         
          if(priority == "low") // 2 dagen van te voren
          {
            DateTime newdt = reservation.Date.AddDays(-2);
            if(newdt <= DateTime.Now){return true;}
            else{return false;}
          }
          if(priority == "medium") // 7 dagen van te voren
          {
            DateTime newdt = reservation.Date.AddDays(-7);
            if(newdt <= DateTime.Now){return true;}
            else{return false;}
          }
          else // high priority kan altijd reserveren
          {
            return true
          }
          
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
