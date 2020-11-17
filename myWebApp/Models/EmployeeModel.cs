using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace myWebApp.Models
{
    public class EmployeeModel
    {
        public int user_id { get; set; }
        public string password { get; set; }
        public string email { get; set; }

    }
}