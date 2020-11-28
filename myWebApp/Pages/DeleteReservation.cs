using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using myWebApp.Database;
using myWebApp.Models;
using Npgsql;


namespace myWebApp.Pages
{
    public class DeleteReservationModel : PageModel
    {
        private readonly ILogger<DeleteReservationModel> _logger;

        public DeleteReservationModel(ILogger<DeleteReservationModel> logger)
        {
            _logger = logger;
        }

        public string Info { get; set; }
        
        public void OnGet(){}

        public List<Reservation> ShowReservations()
        {
            List<Reservation> ReservationList = new List<Reservation>();

            var cs = Database.Database.Connector();

            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "SELECT res_email, date, res_location, res_room FROM reservations";
            using var cmd = new NpgsqlCommand(sql, con);

            NpgsqlDataReader dRead = cmd.ExecuteReader();
           
            while (dRead.Read())
            {
                ReservationList.Add(new Reservation(dRead[0].ToString(),dRead[1].ToString(),dRead[2].ToString()));
            }
            return ReservationList;
        }

        public void OnPostSubmit(ReservationModel reservation){
            DeleteReservation(reservation.res_email, reservation.date);
        }

        public void DeleteReservation(string Email, DateTime Date){
            var cs = Database.Database.Connector();
			using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "DELETE FROM reservations WHERE res_email = @Email AND date = @Date;";
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("Email", Email);
            cmd.Parameters.AddWithValue("Date", Date);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }

    public class Reservation{
        public Reservation(string email, DateTime date){
            Email = email;
            Date = date;
        }
        public string Email {get; set;}
        public string Date {get; set;}

    }
}