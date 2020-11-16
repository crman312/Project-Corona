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


namespace myWebApp.Pages
{
    public class AddWorkspaceModel : PageModel
    {
        private readonly ILogger<AddWorkspaceModel> _logger;

        public AddWorkspaceModel(ILogger<AddWorkspaceModel> logger)
        {
            _logger = logger;
        }

        public string Info { get; set; }
 
        public void OnGet()
        {
        }
 
        public void OnPostSubmit(WorkspaceModel workspace)
        {
            this.Info = string.Format("Info: {0} {1} {2}", workspace.LocationName, workspace.RoomName, workspace.SquareMeters);
            CreateWorkspace(workspace.LocationName, workspace.RoomName, workspace.SquareMeters);
        }

        public void CreateWorkspace(string location, string room, int squaremeters)
        {
            
            var cs = "Host=localhost;Username=postgres;Password=admin;Database=Corona kantoor app";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            
            

            var sql = "INSERT INTO workspaces(location, room, squaremeters) VALUES(@location, @room, @foursquare)";
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("location", location);
            cmd.Parameters.AddWithValue("room", room);
            cmd.Parameters.AddWithValue("squaremeters", squaremeters);

            cmd.Prepare();


            cmd.ExecuteNonQuery();
        }


       
  


        
            
        public void OnPost(object sender, EventArgs e)
        {

            var locationInput = "";
            var roomInput = "";
            var squaremetersInput = "";

            locationInput = Request.Form["Location"];
            roomInput = Request.Form["Room"];
            squaremetersInput = Request.Form["SquareMeters"];

            var cs = "Host=localhost;Username=postgres;Password=admin;Database=Corona kantoor app";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "INSERT INTO workspaces(location, room, squareMeters) VALUES('Washington', 2, 100)"; //@locationInput, @roomInput, @squaremetersInput
            cmd.ExecuteNonQuery(); 
                    
        }
                    
    }
}