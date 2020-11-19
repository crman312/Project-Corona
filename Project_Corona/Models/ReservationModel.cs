using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Project_Corona.Database;
using Project_Corona.Models;
using Npgsql;

namespace Project_Corona.Models
{
    public class ReservationModel
    {
        [BindProperty]
        public int Reservationid {get; set;}
        [BindProperty]
        public DateTime Dayid {get; set;}
        [BindProperty]
        public int Roomid {get; set;}
        [BindProperty]
        public int Employeeid {get; set;}


    }

}