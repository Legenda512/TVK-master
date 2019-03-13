using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TVK.Web.Models;
using Flurl; // почитать
using Flurl.Http;

namespace TVK.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Post(TvkCommand tvkCommand)
        {
            if (tvkCommand == null)
            {
                throw new ArgumentNullException(nameof(tvkCommand));
            }

            var req = new
            {
                command = tvkCommand.Command
            };
            tvkCommand.Data = await tvkCommand.Address.PostJsonAsync(req).ReceiveString();
            return View("Index", tvkCommand);
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
