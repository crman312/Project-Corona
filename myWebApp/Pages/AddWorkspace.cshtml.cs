using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using myWebApp.Database;
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
            Database db = new Database();
        

            var locationInput = "";
            var roomInput = "";
            var squaremetersInput = "";

            locationInput = Request.Form["Location"];
            roomInput = Request.Form["Room"];
            squaremetersInput = Request.Form["SquareMeters"];

            var cs = db.Connection();

            using var con = new NpgsqlConnection(cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "INSERT INTO workspaces(location, room, squareMeters) VALUES( @locationInput, @roomInput, @squaremetersInput)";
            cmd.ExecuteNonQuery(); 
                    
        }
                    
    }
}