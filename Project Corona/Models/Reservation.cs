using System;

using System.ComponentModel.DataAnnotations;

namespace Project_Corona.Models
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