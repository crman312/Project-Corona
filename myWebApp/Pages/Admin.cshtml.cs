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
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace myWebApp.Pages
{
    public class AdminModel : PageModel
    {

        private readonly IWebHostEnvironment _he;

        public AdminModel(IWebHostEnvironment he)
        {
            _he = he;
        }

        [BindProperty]
        public IFormFile Upload { get; set; }

        [BindProperty]
        public IFormFile UploadedFile { get; set; }

        public void OnGet()
        {
        }
        
        public async Task OnPostAsync()
        {
            var file = Path.Combine(_he.ContentRootPath, "Images", Upload.FileName);
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
            }
        }
    

        private readonly ILogger<AdminModel> _logger;

        

        


        public void  OnPostSubmit(NotificationModel notif)
        {
            DateTime datenow = DateTime.Now;
            CreateNotification(datenow, notif.Bericht);
        
        }


        public void CreateNotification(DateTime convdayid, string Bericht) 
        {
            var cs = Database.Database.Connector();

            using var con = new NpgsqlConnection(cs);
            con.Open();


            var sql = "INSERT INTO notification(bericht, datumnu) VALUES(@Msg, @Date)";
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("Msg", Bericht);
            cmd.Parameters.AddWithValue("Date", convdayid);
            
            

            cmd.Prepare();

            cmd.ExecuteNonQuery();
            con.Close(); 
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

        public void OnPostUpload()
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
        }
    }
}