using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
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
        public string userEmail {get; set;}
        public string Info { get; set; }
        
        public void OnGet(){}

        public List<Reservations> ShowReservations()
        {
            userEmail = HttpContext.Session.GetString("useremail");
            List<Reservations> Reservations = new List<Reservations>();

            var cs = Database.Database.Connector();

            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "SELECT res_email, date, res_location, res_room FROM reservations WHERE res_email = @Email ORDER BY date ASC, res_location ASC";
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("Email", userEmail);
            NpgsqlDataReader dRead = cmd.ExecuteReader();
           
            while (dRead.Read())
            {
                Reservations.Add(new Reservations(dRead[0].ToString(),dRead[1].ToString(),dRead[2].ToString(), dRead[3].ToString()));
            }
            return Reservations;
        }

        public void OnPostRemove(ReservationModel reservation)
        {
            userEmail = HttpContext.Session.GetString("useremail");
            
            DateTime convdayid = Convert.ToDateTime(reservation.Date);
            DeleteReservation(convdayid, userEmail);
        }

        public bool CheckReservation(DateTime convdayid, string Email) 
        {   
            int AmountDate = 0;
            var cs = Database.Database.Connector();
            List<DateTime> res = new List<DateTime>();
            using var con = new NpgsqlConnection(cs);
            {
                string query = "Select date FROM reservation WHERE res_email = '"+ Email+"'";
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
                if (p == convdayid)
                {
                    AmountDate++;
                }
            }
            if (AmountDate >= 1)
            {
                return false;
            }
            else {return true;}
        }

        public void DeleteReservation(DateTime date, string email)
        {
            
            var cs = Database.Database.Connector();
            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "DELETE FROM reservations WHERE res_email = @email AND date = @date";
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("email", email);
            cmd.Parameters.AddWithValue("date", date);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            con.Close();

        }
    }
}