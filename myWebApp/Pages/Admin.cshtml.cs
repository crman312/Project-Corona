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

        public void OnPostSubmit(UploadFileModel logoFile)
        {

            UploadLogo(logoFile.Logo);
        }

        public void UploadLogo(IFormFile File)
        {
            var cs = Database.Database.Connector();

            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "INSERT INTO logo(file) VALUES(@File)";
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("file", File);
    
            cmd.Prepare();

            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}