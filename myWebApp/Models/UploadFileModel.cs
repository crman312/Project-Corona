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
using Microsoft.AspNetCore.Http;


namespace myWebApp.Models
{
    public class UploadFileModel
    {
        [BindProperty]
        public IFormFile Logo { get; set; }
    }
}