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
    public class WorkspaceModel
    {
        [BindProperty]
        public string LocationName { get; set; }
        [BindProperty]
        public string RoomName { get; set; } 
        [BindProperty]
        public int SquareMeters { get; set; } = 1;
        [BindProperty]
        public int Lengthws { get; set; } = 1;
        [BindProperty]
        public int Widthws { get; set; } = 1;
    }
}

