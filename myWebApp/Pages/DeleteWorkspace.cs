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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;


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
        public string userEmail { get; set; }

        
        public void OnGet()
        {
            userEmail = HttpContext.Session.GetString("useremail");
        }

        public List<Workspace> ShowWorkspaces()
        {
            List<Workspace> Workspaces = new List<Workspace>();

            var cs = Database.Database.Connector();

            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "SELECT location, room, squaremeters, availableworkspaces, workspace_id FROM workspaces ORDER BY workspace_id ASC";
            using var cmd = new NpgsqlCommand(sql, con);

            NpgsqlDataReader dRead = cmd.ExecuteReader();
           
            while (dRead.Read())
            {
                Workspaces.Add(new Workspace(dRead[0].ToString(),dRead[1].ToString(),dRead[2].ToString(),dRead[3].ToString(),Convert.ToInt32(dRead[4])));
            }
            return Workspaces;
        }  

        public void OnPostSubmit(WorkspaceModel workspace){
            userEmail = HttpContext.Session.GetString("useremail");
            DeleteWorkspace(workspace.Id);
        }

        public void DeleteWorkspace(int id){
            var cs = Database.Database.Connector();
			using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "DELETE FROM workspaces WHERE workspace_id = @id;";
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }
    public class Workspace{
        public Workspace(string location, string room, string squaremeters, string availableworkspaces, int id){
            WSlocation = location;
            WSroom = room;
            WSsquaremeters= squaremeters;
            WSavailableworkspaces = availableworkspaces;
            WSid = id;
        }
        public string WSlocation {get; set;}
        public string WSroom {get; set;}
        public string WSsquaremeters {get; set;}
        public string WSavailableworkspaces {get; set;}
        public int WSid {get; set;}
    }
}