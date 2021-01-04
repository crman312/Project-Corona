using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using myWebApp.Database;
using myWebApp.Models;
using myWebApp.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Npgsql;

namespace myWebApp.Pages
{
  public class PrioritiesModel : PageModel
  {
    private readonly ILogger<PrioritiesModel> _logger;

    public PrioritiesModel(ILogger<PrioritiesModel> logger)
    {
      _logger = logger;
    }
    public void OnGet()
    {
    }

    public string Info { get; set; }

    public void OnPostSubmit(PrioModel prio)
    {
        bool check = CheckIfExist();        

        int id = 1;
        if(check == false)
        {
            CreatePriorities(id, prio.High, prio.Medium, prio.Low);
            this.Info = string.Format("Successfully saved");
        }
        if(check == true)
        {
            UpdatePriorities(id, prio.High, prio.Medium, prio.Low);
            this.Info = string.Format("Successfully updated");
        }
    }

    public static bool CheckIfExist()
    {
        var cs = Database.Database.Connector();

        using var con = new NpgsqlConnection(cs);
        con.Open();

        var sql = "SELECT * FROM priorities";
        using var cmd = new NpgsqlCommand(sql, con);

        NpgsqlDataReader dRead = cmd.ExecuteReader();
           
        while (dRead.Read())
        {
            for(int i = 0; i < dRead.FieldCount; i++)
            {
                if(dRead[0].ToString() == "1")
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void CreatePriorities(int Prioid, int High, int Medium, int Low)
    {
        var cs = Database.Database.Connector();

        using var con = new NpgsqlConnection(cs);
        con.Open();

        var sql = "INSERT INTO priorities(prioid, high, medium, low) VALUES(@Prioid, @High, @Medium, @Low)";
        using var cmd = new NpgsqlCommand(sql, con);
        cmd.Parameters.AddWithValue("prioid", Prioid);
        cmd.Parameters.AddWithValue("high", High);
        cmd.Parameters.AddWithValue("medium", Medium);
        cmd.Parameters.AddWithValue("low", Low);

        cmd.Prepare();

        cmd.ExecuteNonQuery();
        con.Close();
    }
    
    public void UpdatePriorities(int Prioid, int High, int Medium, int Low)
    {
        var cs = Database.Database.Connector();

        using var con = new NpgsqlConnection(cs);
        con.Open();

        var sql = "UPDATE priorities SET high = @High, medium = @Medium, low = @Low WHERE prioid = @Prioid";
        using var cmd = new NpgsqlCommand(sql, con);
        
        cmd.Parameters.Add(new NpgsqlParameter("@prioid", Prioid));
        cmd.Parameters.Add(new NpgsqlParameter("@high", High));
        cmd.Parameters.Add(new NpgsqlParameter("@medium", Medium));
        cmd.Parameters.Add(new NpgsqlParameter("@low", Low));

        cmd.ExecuteNonQuery();
        cmd.Dispose();  

        con.Close();
    }

    public static Tuple<int, int, int> GetPriorities()
    {
        var cs = Database.Database.Connector();

        using var con = new NpgsqlConnection(cs);
        con.Open();

        var sql = "SELECT * FROM priorities";
        using var cmd = new NpgsqlCommand(sql, con);

        NpgsqlDataReader dRead = cmd.ExecuteReader();
           
        while (dRead.Read())
        {
            for(int i = 0; i < dRead.FieldCount; i++)
            {
                if(dRead[0].ToString() == "1")
                {
                    return Tuple.Create(Convert.ToInt32(dRead[1]), Convert.ToInt32(dRead[2]), Convert.ToInt32(dRead[3]));
                }
            }
        }
        return null;
    }
  }
}
