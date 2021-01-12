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
            int availworkspaces = AvailableWorkspaces(workspace.SquareMeters, workspace.Lengthws, workspace.Widthws);

            this.Info = string.Format("Successfully saved, {0} people can work in this room", availworkspaces);

            CreateWorkspace(workspace.LocationName, workspace.RoomName, workspace.SquareMeters, availworkspaces);
        }

        public int AvailableWorkspaces(int SquareMeters, double x, double y)
        {
            double X = 1.5 + x;
            double Y = 1.5 + y;

            double squareX = Math.Sqrt(SquareMeters) / X;
            double squareY = Math.Sqrt(SquareMeters) / Y;

            int total = Convert.ToInt32(squareX) * Convert.ToInt32(squareY);
            return total;
        }

        public void CreateWorkspace(string Location, string Room, int Squaremeters, int Availableworkspaces)
        { 
            var cs = Database.Database.Connector();

            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "INSERT INTO workspaces(location, room, squaremeters, availableworkspaces) VALUES(@Location, @Room, @Squaremeters, @Availableworkspaces)";
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("location", Location);
            cmd.Parameters.AddWithValue("room", Room);
            cmd.Parameters.AddWithValue("squaremeters", Squaremeters);
            cmd.Parameters.AddWithValue("availableworkspaces", Availableworkspaces);

            cmd.Prepare();

            cmd.ExecuteNonQuery();
            con.Close();
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
    }
}