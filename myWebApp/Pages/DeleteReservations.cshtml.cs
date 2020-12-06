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
                Reservations.Add(new Reservations(dRead[0].ToString(),dRead[1].ToString(),dRead[2].ToString(), dRead[3].ToString()));
            }
            return Reservations;
        }

        public void OnPostSubmit(ReservationModel reservation){
            DeleteReservation(reservation.Email, reservation.Date);
        }

        public void DeleteReservation(string Email, DateTime Date){
            var cs = Database.Database.Connector();
			using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "DELETE FROM reservations WHERE res_email = @email AND date = @date;";
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("email", Email);
            cmd.Parameters.AddWithValue("date", Date);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }


}