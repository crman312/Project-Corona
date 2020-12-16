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
    public class NotificationModel
    {
        [BindProperty]
        public string Bericht { get; set; } 
        [BindProperty]
        public string Datenow { get; set; } 

        

        
    }
}