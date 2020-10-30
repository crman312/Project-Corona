using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Npgsql;


namespace myWebApp.Pages
{
    public class AddWorkspaceModel : PageModel
    {
        private readonly ILogger<AddWorkspaceModel> _logger;

        public AddWorkspaceModel(ILogger<AddWorkspaceModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        
            
        public void OnPost(object sender, EventArgs e)
        {


            string locationInput = Request.Form["Location"];;
            string roomInput = Request.Form["Room"];
            string squaremetersInput = Request.Form["SquareMeters"];

            var cs = "Host=localhost;Username=postgres;Password=admin;Database=Corona kantoor app";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "INSERT INTO workspaces(location, room, squareMeters) VALUES('locationInput', 'locationInput', 1)";
            cmd.ExecuteNonQuery(); 
                    
        }
                    
    }
}