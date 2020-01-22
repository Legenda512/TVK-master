using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TVK.Web.Models;
using Flurl.Http;

namespace TVK.Web.Controllers
{
    public class ScreenRecorderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Post(ScreenRecorder ScreenRecorder)
        {


            if (ScreenRecorder == null)
            {
                throw new ArgumentNullException(nameof(ScreenRecorder));
            }

            var req = new
            {
                command = ScreenRecorder.Command
            };
            ScreenRecorder.Data = await ScreenRecorder.Address.PostJsonAsync(req).ReceiveString();


            return View("Index", ScreenRecorder);
        }

    }
}