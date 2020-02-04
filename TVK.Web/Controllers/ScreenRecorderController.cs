using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TVK.Web.Models;
using Flurl.Http;
using TVK.Client.Daemon.Web.Models;

namespace TVK.Web.Controllers
{
    [Route("[controller]")]
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

            ScreenRecorder.Data = await ScreenRecorder.Address.PostJsonAsync(ScreenRecorder).ReceiveString();


            return View("Index", ScreenRecorder);
        }

    }
}