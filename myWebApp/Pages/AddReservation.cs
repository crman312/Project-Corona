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

            
            this.Info = string.Format("Sucessfully added the reservation");

            CreateReservation(reservation.email, reservation.Date);
        }
        public void CreateReservation(string email, DateTime Date)
        {
            var cs = Database.Database.Connector();

            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "INSERT INTO reservations(email, date) VALUES(@email, @date)";
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("email", email);
            cmd.Parameters.AddWithValue("date", Date);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
            con.Close();   
        }  
    }
}
