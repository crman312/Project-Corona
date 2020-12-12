using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using myWebApp.Database;
using myWebApp.Models;
using Npgsql;
using Microsoft.AspNetCore.Http;
using System.IO;


namespace myWebApp.Pages
{
    public class AdminModel : PageModel
    {
        private readonly ILogger<AdminModel> _logger;

        public void OnGet()
        {
        }

        public AdminModel(ILogger<AdminModel> logger)
        {
            _logger = logger;
        }
        
        

        [BindProperty]
        public IFormFile UploadedFile { get; set; }


        public async Task OnPostAsync()
        {
            var file = @"wwwroot/Images/logo" + UploadedFile.FileName;
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await UploadedFile.CopyToAsync(fileStream);
            }
        }

        /*public void OnPostSubmit()
        {
            UploadLogo(UploadedFile);
        }

        public void UploadLogo(IFormFile file)
        {
            var cs = Database.Database.Connector();

            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "INSERT INTO logo(file) VALUES(@file)";
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("file", file);
    
            cmd.Prepare();

            cmd.ExecuteNonQuery();
            con.Close();
        }*/
    }
}