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
        // Create
        public void OnPostSubmit(ReservationModel reservation)
        {
            

            this.Info = string.Format("Reservation successfully saved");

            CreateReservation(reservation.Reservationid, reservation.Dayid, reservation.Roomid, reservation.Employeeid);
        }

        public void CreateReservation(int Reservationid, DateTime Dayid, int Roomid, int Employeeid)
        { 
            var cs = Database.Database.Connector();

            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "INSERT INTO reservation(reservationid, dayid, roomid, employeeid) VALUES(@reservationid, @dayid, @roomid, @employeeid)";
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("reservationid", Reservationid);
            cmd.Parameters.AddWithValue("dayid", Dayid);
            cmd.Parameters.AddWithValue("roomid", Roomid);
            cmd.Parameters.AddWithValue("employeeid", Employeeid);

            cmd.Prepare();

            cmd.ExecuteNonQuery();
            con.Close();   
        }

        

    }
}