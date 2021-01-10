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
  public class OpeningHoursModel : PageModel
  {
    private readonly ILogger<OpeningHoursModel> _logger;

    public OpeningHoursModel(ILogger<OpeningHoursModel> logger)
    {
      _logger = logger;
    }
    public void OnGet()
    {
    }

    public string Info { get; set; }

    public void OnPostSubmit(OpeningModel hours)
    {
        bool check = CheckIfExist();        

        int id = 1;
        if(check == false)
        {
            CreateOpeningshours(id, hours.Monday, hours.Tuesday, hours.Wednesday, hours.Thursday, hours.Friday, hours.Saturday, hours.Sunday);
            this.Info = string.Format("Successfully saved");
        }
        if(check == true)
        {
            UpdateOpeningshours(id, hours.Monday, hours.Tuesday, hours.Wednesday, hours.Thursday, hours.Friday, hours.Saturday, hours.Sunday);
            this.Info = string.Format("Successfully updated");
        }
    }

    public static bool CheckIfExist()
    {
        var cs = Database.Database.Connector();

        using var con = new NpgsqlConnection(cs);
        con.Open();

        var sql = "SELECT * FROM openinghours";
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

    public void CreateOpeningshours(int Id, string Monday, string Tuesday, string Wednesday, string Thursday, string Friday, string Saturday, string Sunday)
    {
        var cs = Database.Database.Connector();

        using var con = new NpgsqlConnection(cs);
        con.Open();

        var sql = "INSERT INTO openinghours(id, monday, tuesday, wednesday, thursday, friday, saturday, sunday) VALUES(@Id, @Monday, @Tuesday, @Wednesday, @Thursday, @Friday, @Saturday, @Sunday)";
        using var cmd = new NpgsqlCommand(sql, con);
        cmd.Parameters.AddWithValue("id", Id);
        cmd.Parameters.AddWithValue("monday", Monday);
        cmd.Parameters.AddWithValue("tuesday", Tuesday);
        cmd.Parameters.AddWithValue("wednesday", Wednesday);
        cmd.Parameters.AddWithValue("thursday", Thursday);
        cmd.Parameters.AddWithValue("friday", Friday);
        cmd.Parameters.AddWithValue("saturday", Saturday);
        cmd.Parameters.AddWithValue("sunday", Sunday);

        cmd.Prepare();

        cmd.ExecuteNonQuery();
        con.Close();
    }
    
    public void UpdateOpeningshours(int Id, string Monday, string Tuesday, string Wednesday, string Thursday, string Friday, string Saturday, string Sunday)
    {
        var cs = Database.Database.Connector();

        using var con = new NpgsqlConnection(cs);
        con.Open();

        var sql = "UPDATE openinghours SET monday = @Monday, tuesday = @Tuesday, wednesday = @Wednesday, thursday = @Thursday, friday = @Friday, saturday = @Saturday, sunday = @Sunday WHERE id = @Id";
        using var cmd = new NpgsqlCommand(sql, con);
        
        cmd.Parameters.Add(new NpgsqlParameter("@id", Id));
        cmd.Parameters.Add(new NpgsqlParameter("@monday", Monday));
        cmd.Parameters.Add(new NpgsqlParameter("@tuesday", Tuesday));
        cmd.Parameters.Add(new NpgsqlParameter("@wednesday", Wednesday));
        cmd.Parameters.Add(new NpgsqlParameter("@thursday", Thursday));
        cmd.Parameters.Add(new NpgsqlParameter("@friday", Friday));
        cmd.Parameters.Add(new NpgsqlParameter("@saturday", Saturday));
        cmd.Parameters.Add(new NpgsqlParameter("@sunday", Sunday));

        cmd.ExecuteNonQuery();
        cmd.Dispose();  

        con.Close();
    }

    public static Tuple<string, string, string, string, string, string, string> GetOpeningHours()
    {
        var cs = Database.Database.Connector();

        using var con = new NpgsqlConnection(cs);
        con.Open();

        var sql = "SELECT * FROM openinghours";
        using var cmd = new NpgsqlCommand(sql, con);

        NpgsqlDataReader dRead = cmd.ExecuteReader();
           
        while (dRead.Read())
        {
            for(int i = 0; i < dRead.FieldCount; i++)
            {
                if(dRead[0].ToString() == "1")
                {
                    return Tuple.Create(dRead[1].ToString(), dRead[2].ToString(), dRead[3].ToString(), dRead[4].ToString(), dRead[5].ToString(), dRead[6].ToString(), dRead[7].ToString());
                }
            }
        }
        return null;
    }
  }
}
