using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using myWebApp.Controllers;
using myWebApp.Database;
using myWebApp.Models;
using myWebApp.Pages;
using Npgsql;

namespace myWebApp.Hubs
{
     
    public class speedalarmhub : Hub
    {
        private IMemoryCache _cache;
        private IHubContext<speedalarmhub> _hubContext;
         public speedalarmhub(IMemoryCache cache, IHubContext<speedalarmhub> hubContext)
        {
            _cache = cache;
            _hubContext = hubContext; 
        }

        public async Task SendMessage()
        {
            if (!_cache.TryGetValue("notification", out string response))
            {
                SpeedListener speedlist = new SpeedListener(_hubContext,_cache);
                speedlist.ListenForAlarmNotifications();
                string jsonspeedalarm = speedlist.GetAlarmList();
                _cache.Set("notification", jsonspeedalarm);
                await Clients.All.SendAsync("ReceiveMessage", _cache.Get("notification").ToString());
            }
            else
            {
                await Clients.All.SendAsync("ReceiveMessage", _cache.Get("notification").ToString());
            }
        }

    }
}