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
    public class OpeningModel
    {
        [BindProperty]
        public string Monday { get; set; }
        
        [BindProperty]
        public string Tuesday { get; set; } 
        
        [BindProperty]
        public string Wednesday { get; set; } 
        
        [BindProperty]
        public string Thursday { get; set; } 
        
        [BindProperty]
        public string Friday { get; set; } 
        
        [BindProperty]
        public string Saturday { get; set; } 

        [BindProperty]
        public string Sunday { get; set; } 
    }
}