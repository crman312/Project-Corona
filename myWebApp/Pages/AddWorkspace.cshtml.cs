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

        public void CreateWorkspace(string Location, string Room, int Squaremeters)
        {
            
            var cs = "Host=localhost;Username=postgres;Password=admin;Database=Corona kantoor app";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            
            

            var sql = "INSERT INTO workspaces(location, room, squaremeters) VALUES(@Location, @Room, @Squaremeters)";
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("location", Location);
            cmd.Parameters.AddWithValue("room", Room);
            cmd.Parameters.AddWithValue("squaremeters", Squaremeters);

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