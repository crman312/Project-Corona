using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Project_Corona.Database;
using Project_Corona.Models;
using Npgsql;

namespace Project_Corona.Controllers
{
    public class HomeController : Controller
    {


        public IActionResult Employeepage()
        {
            List<WorkspaceModel> res = PopulateReservations();
            return View(new SelectList(res, "LocationName"));
        } 

        private static List<WorkspaceModel> PopulateReservations()
        {
            var cs = Database.Database.Connector();
            List<WorkspaceModel> res = new List<WorkspaceModel>();
            using var con = new NpgsqlConnection(cs);
            {
                string query = "Select location FROM workspaces";
                using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                {
                    cmd.Connection = con;
                    con.Open();
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            res.Add(new WorkspaceModel{LocationName = dr["LocationName"].ToString()});
                        }
                    }
                    con.Close();
                }
            }
        
            return res;
        }
      
    }
    

}