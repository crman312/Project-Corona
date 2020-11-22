using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using myWebApp.Database;
using myWebApp.Models;
using Npgsql;


namespace myWebApp.Pages
{
    public class DeleteWorkspaceModel : PageModel
    {
        private readonly ILogger<DeleteWorkspaceModel> _logger;

        public DeleteWorkspaceModel(ILogger<DeleteWorkspaceModel> logger)
        {
            _logger = logger;
        }

        public string Info { get; set; }
        
        public void OnGet(){
        }

        public List<Workspace> ShowWorkspaces()
        {
            List<Workspace> Workspaces = new List<Workspace>();

            var cs = Database.Database.Connector();

            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "SELECT location, room, squaremeters, availableworkspaces FROM workspaces";
            using var cmd = new NpgsqlCommand(sql, con);

            NpgsqlDataReader dRead = cmd.ExecuteReader();
           
            while (dRead.Read())
            {
                Workspaces.Add(new Workspace(dRead[0].ToString(),dRead[1].ToString(),dRead[2].ToString(),dRead[3].ToString()));
            }
            return Workspaces;
        }  

        public void OnPostSubmit(WorkspaceModel workspace){
            DeleteWorkspace(workspace.LocationName, workspace.RoomName);
        }

        public void DeleteWorkspace(string locationName, string roomName){
            var cs = Database.Database.Connector();
			using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "DELETE FROM workspaces WHERE location = @locationname AND room = @roomname;";
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("locationname", locationName);
            cmd.Parameters.AddWithValue("roomname", roomName);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }
    public class Workspace{
        public Workspace(string location, string room, string squaremeters, string availableworkspaces){
            WSlocation = location;
            WSroom = room;
            WSsquaremeters= squaremeters;
            WSavailableworkspaces = availableworkspaces;
        }
        public string WSlocation {get; set;}
        public string WSroom {get; set;}
        public string WSsquaremeters {get; set;}
        public string WSavailableworkspaces {get; set;}
    }
}
      
    