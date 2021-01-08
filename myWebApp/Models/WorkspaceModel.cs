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

namespace myWebApp.Models
{
    public class WorkspaceModel
    {
        [BindProperty]
        public string LocationName { get; set; }
        [BindProperty]
        public string RoomName { get; set; } 
        [BindProperty]
        public int SquareMeters { get; set; } = 1;
        [BindProperty]
        public double Lengthws { get; set; } = 1;
        [BindProperty]
        public double Widthws { get; set; } = 1;
        [BindProperty]
        public int Id {get; set;}
    }
}