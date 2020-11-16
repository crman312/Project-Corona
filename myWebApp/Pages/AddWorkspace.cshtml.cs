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

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;

            /*cmd.CommandText = "INSERT INTO workspaces(location, room, squaremeters) VALUES(@location, @room, @foursquare)";
            cmd.Prepare();
            cmd.ExecuteNonQuery(); */

            private NpgsqlDataAdapter NpAdapter;
            string insertQuery = "INSERT INTO workspaces(location, room, squaremeters) VALUES(@location, @room, @squaremeters)";
            NpAdapter.InsertCommand = new NpgsqlCommand(insertQuery, con);

            NpParam = NpAdapter.InsertCommand.Parameters.Add("@location", NpgsqlTypes.NpgsqlDbType.Text);
            NpParam.SourceColumn = "location";
            NpParam.SourceVersion = DataRowVersion.Current;

            NpParam = NpAdapter.InsertCommand.Parameters.Add("@room", NpgsqlTypes.NpgsqlDbType.Bigint);
            NpParam.SourceVersion = DataRowVersion.Current;
            NpParam.SourceColumn = "room";
            
            NpParam = NpAdapter.InsertCommand.Parameters.Add("@squaremeters", NpgsqlTypes.NpgsqlDbType.Bigint);
            NpParam.SourceVersion = DataRowVersion.Current;
            NpParam.SourceColumn = "squaremeters";

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