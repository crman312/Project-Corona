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
        public string Dayid {get; set;}
        [BindProperty]
        public string Roomid {get; set;}
        [BindProperty]
        public string Email {get; set;}
        [BindProperty]
        public string Locationid {get; set;}


    }

}