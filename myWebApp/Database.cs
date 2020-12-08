using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace myWebApp.Database
{
    public class Database
    {

        public static string Connector()
        {
            string cs = "Host=localhost;Username=postgres;Password=123456;Database=Login";
            return cs;
        }


    }

}