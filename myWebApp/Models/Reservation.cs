using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

namespace myWebApp.Models
{
    public class Reservation
    {
        [Key]
        public int reservationid {get; set;}
        
        public DateTime dayid {get; set;}
        public int roomid {get; set;}
        public int employeeid {get; set;}
    }
}