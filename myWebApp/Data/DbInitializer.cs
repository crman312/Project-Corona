using myWebApp.Data;
using myWebApp.Models;
using System;
using System.Linq;

namespace myWebApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ConnectionstringClass context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Reservation.Any())
            {
                return;   // DB has been seeded
            }

            var reservations = new Reservation[]
            {
                new Reservation{reservationid=1, dayid=DateTime.Parse("2020-11-16"), roomid=1, employeeid=1},
                
            };

            context.Reservation.AddRange(reservations);
            context.SaveChanges();
        }
    }
}