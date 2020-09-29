using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Chat.Models;
using Chat.Hubs;
using Microsoft.AspNetCore.SignalR;
using Chat.Data;
using Microsoft.AspNetCore.Authorization;

namespace Chat.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, IServiceProvider serviceProvider, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var message = _dbContext.Messages.FirstOrDefault();                       

            ViewBag.listaDeMensagens = _dbContext.Messages.OrderByDescending(x=>x.TimeStamp).Take(50).OrderBy(x=>x.TimeStamp).ToList();

            return View();
        }

        public IActionResult Privacy()
        {   
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {            
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
