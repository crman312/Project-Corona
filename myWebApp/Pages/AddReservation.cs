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
  public class AddReservationModel : PageModel
  {
    private readonly ILogger<AddReservationModel> _logger;

    public AddReservationModel(ILogger<AddReservationModel> logger)
    {
      _logger = logger;
    }

    [BindProperty]
    public List<WorkspaceModel> locations {get; set;}
    [BindProperty]
    public List<WorkspaceModel> rooms {get; set;}
    public string Info { get; set; }
    public string userEmail { get; set; }


    public void OnGet()
    {
      locations = ShowLocations();
      rooms = ShowRooms();
    }

    public void OnPostSubmit(ReservationModel reservation)
    {
      DateTime convdayid = Convert.ToDateTime(reservation.Date);
      userEmail = HttpContext.Session.GetString("useremail");

      bool check = prioCheck(reservation);
      bool check1 = CheckReservation(convdayid, userEmail);
      bool check2 = CheckRoomAvailability(reservation);
      bool check3 = CheckRoomOpen(convdayid);
      if(check && check1 && check2 && check3){
        CreateReservation(userEmail, convdayid, reservation.Location, reservation.Room);
        this.Info = string.Format("Sucessfully added the reservation");
      }
      else{
        if (!check1) {
        this.Info = string.Format("You already have a reservation for this day, or you tried to reserve in the past, try a different date.");
        }
        if(!check) { 
          this.Info = string.Format("You do not have the right priority, please try a later date.");
        }
        if(!check2) {
          this.Info = string.Format("The room you tried to reserve is full!");
        }
        if(!check3) {
          this.Info = string.Format("This location is closed on this day, try another day");
        }
      }
    }

    public bool prioCheck(ReservationModel reservation)
    {
      int med = 7;
      int low = 2;
      int high = 0;
      bool check = PrioritiesModel.CheckIfExist();
      if(check)
      {
        Tuple<int, int, int> getprio = PrioritiesModel.GetPriorities();
        med = getprio.Item2;
        low = getprio.Item3;
        high = getprio.Item1;
      }

      DateTime convdayid = Convert.ToDateTime(reservation.Date);
      userEmail = HttpContext.Session.GetString("useremail");
          
      var cs = Database.Database.Connector(); // start connectie met database

      List<string> pr = new List<string>(); //list creeren

      using var con = new NpgsqlConnection(cs);
      {
        string query = "Select priority FROM employees WHERE email = '"+ userEmail+"'";
        using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
        {
          cmd.Connection = con;
          con.Open();
          using (NpgsqlDataReader dr = cmd.ExecuteReader())
          {
            while (dr.Read())
            {
              pr.Add(((string) dr["priority"]));
            }
          }
          con.Close(); //sluit de connection, maar de list pr bestaat nog
        }
      }

      foreach(string priority in pr)
      {
        if(priority == "Low") // 2 dagen van te voren
        {
          DateTime newdt = convdayid.AddDays(-(low));
          if(newdt <= DateTime.Now){return true;}
          else{return false;}
        }
        else if(priority == "Medium") // 7 dagen van te voren
        {
          DateTime newdt = convdayid.AddDays(-(med));
          if(newdt <= DateTime.Now){return true;}
          else{return false;}
        }
        else // high priority kan altijd reserveren
        {
          if(high != 0)
          {
            DateTime newdt = convdayid.AddDays(-(high));
            if(newdt <= DateTime.Now){return true;}
            else{return false;}
          }
          return true;
        }
      }
      return false;
    }

    public void CreateReservation(string Email, DateTime Date, string Location, string Room)
    {
      var cs = Database.Database.Connector();

      using var con = new NpgsqlConnection(cs);
      con.Open();

      var sql = "INSERT INTO reservations(res_email, date, res_location, res_room) VALUES(@email, @date, @location, @room)";
      using var cmd = new NpgsqlCommand(sql, con);
      cmd.Parameters.AddWithValue("email", Email);
      cmd.Parameters.AddWithValue("date", Date);
      cmd.Parameters.AddWithValue("location", Location);
      cmd.Parameters.AddWithValue("room", Room);
      cmd.Prepare();

      cmd.ExecuteNonQuery();
      con.Close();   
    }  

    public List<Reservations> ShowReservations()
    {
      userEmail = HttpContext.Session.GetString("useremail");
      List<Reservations> Reservations = new List<Reservations>();

      var cs = Database.Database.Connector();

      using var con = new NpgsqlConnection(cs);
      con.Open();

      var sql = "SELECT res_email, date, res_location, res_room FROM reservations WHERE res_email = '"+userEmail+"' ORDER BY date ASC, res_location ASC";
      using var cmd = new NpgsqlCommand(sql, con);

      NpgsqlDataReader dRead = cmd.ExecuteReader();
           
      while (dRead.Read())
      {
        Reservations.Add(new Reservations(dRead[0].ToString(),dRead[1].ToString(),dRead[2].ToString(),dRead[3].ToString()));
      }
      return Reservations;
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

    public bool CheckReservation(DateTime convdayid, string Email) 
    {   
      int AmountDate = 0;
      DateTime now = DateTime.Now;
           
      var cs = Database.Database.Connector();
      List<DateTime> res = new List<DateTime>();
      using var con = new NpgsqlConnection(cs);
      {
        string query = "Select date FROM reservations WHERE res_email = '"+ Email+"'";
        using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
        {
          cmd.Connection = con;
          con.Open();
          using (NpgsqlDataReader dr = cmd.ExecuteReader()){
            while (dr.Read()){
              res.Add(((DateTime) dr["date"]));
            }        
          }      
        }
      }

      foreach(DateTime p in res){
        if (p == convdayid){
          AmountDate++;    
        }
      }

      if (AmountDate >= 1){
        return false;
      }
      else{
        return true;
      }
    }
    public bool CheckRoomAvailability(ReservationModel reservation){
      var cs = Database.Database.Connector();
      using var con = new NpgsqlConnection(cs);
      con.Open();

      var sql = "SELECT COUNT(*) FROM reservations WHERE res_location = '"+reservation.Location+"' AND res_room = '"+reservation.Room+"' AND date = '"+reservation.Date+"'";
      using var cmd = new NpgsqlCommand(sql, con);

      int roomReservations = Convert.ToInt32(cmd.ExecuteScalar());
      sql = "Select availableworkspaces FROM workspaces WHERE location = '"+reservation.Location+"' AND room = '"+reservation.Room+"'";
      using var cmd2 = new NpgsqlCommand(sql, con);
      int roomAvailableSpaces = Convert.ToInt32(cmd2.ExecuteScalar());

      if(roomReservations < roomAvailableSpaces){ return true;}
      else{ return false;}
    }
    public bool CheckRoomOpen(DateTime date){
      int Day = (int)date.DayOfWeek;
      var cs = Database.Database.Connector();
      using var con = new NpgsqlConnection(cs);
      con.Open();

      var sql = "Select * FROM openinghours";
      using var cmd = new NpgsqlCommand(sql, con);
      NpgsqlDataReader dr = cmd.ExecuteReader();
      string open = "";
      while(dr.Read()){
        if(Day == 0){ open = dr[7].ToString();}
        else{ open = dr[Day].ToString();}
      }
      //this.Info = string.Format($"Day = {Day} open = {open}");
      if(open.ToLower() == "closed"){ return false;}
      else{ return true;}
    }
  public List<WorkspaceModel> ShowLocations()
    {
        var cs = Database.Database.Connector();
        List<WorkspaceModel> res = new List<WorkspaceModel>();
        using var con = new NpgsqlConnection(cs);
        {
            string query = "Select DISTINCT location FROM workspaces";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            {
                cmd.Connection = con;
                con.Open();
                using (NpgsqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        res.Add(new WorkspaceModel { LocationName = dr["location"].ToString() });
                    }
                }
                con.Close();
            }
        }
        return res;
    }
    public List<WorkspaceModel> ShowRooms()
    {
      var cs = Database.Database.Connector();
      List<WorkspaceModel> res = new List<WorkspaceModel>();
      using var con = new NpgsqlConnection(cs);
      {
        string query = "Select room FROM workspaces";
        using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
        {
          cmd.Connection = con;
          con.Open();
          using (NpgsqlDataReader dr = cmd.ExecuteReader())
          {
            while (dr.Read())
              {
                res.Add(new WorkspaceModel { RoomName = dr["room"].ToString() });
              }
            }    
          con.Close();
        }
      }
      return res;
    }
    public IActionResult OnPostShowRoom(string loc)
    {
      List<WorkspaceModel> l= new List<WorkspaceModel>();
      var cs = Database.Database.Connector();
      using var con = new NpgsqlConnection(cs);
      {
        string query = "Select room FROM workspaces WHERE location = '"+ loc +"'";
        using NpgsqlCommand cmd = new NpgsqlCommand(query, con);
        {
          cmd.Connection = con;
          con.Open();
          using (NpgsqlDataReader dr = cmd.ExecuteReader())
          {
            while (dr.Read())
            {
              l.Add(new WorkspaceModel { RoomName = dr["room"].ToString() });
            }
          }
          con.Close();
        }
      }
      return new JsonResult(l);
    }
  }
}

  
  public class Reservations
  {
    public Reservations(string email, string date, string location, string room)
    {
      Email = email;
      Date = date;
      Location = location;
      Room = room;
    }
    public string Email {get; set;}
    public string Date {get; set;}
    public string Location {get; set;}
    public string Room { get; set; }
  }
