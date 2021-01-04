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
    public class PrioModel
    {
        [BindProperty]
        public int High { get; set; } 
        [BindProperty]
        public int Medium { get; set; } 

        [BindProperty]
        public int Low { get; set; }
    }
}