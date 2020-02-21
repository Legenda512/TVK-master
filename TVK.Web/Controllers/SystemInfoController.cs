using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TVK.Web.Models;
using Flurl.Http;
using TVK.Client.Daemon.Web.Models;
using System.Threading;

namespace TVK.Web.Controllers
{
    [Route("[controller]")]
    public class SystemInfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Post(GetMonitorSystem GetMonitorSystem)
        {


            if (GetMonitorSystem == null)
            {
                throw new ArgumentNullException(nameof(GetMonitorSystem));
            }


            GetMonitorSystem.Data = await GetMonitorSystem.Address.PostJsonAsync(GetMonitorSystem).ReceiveString();


            return View("Index", GetMonitorSystem);



            //var req = new
            //{
            //    command = GetMonitorSystem.Command
            //};
            //GetMonitorSystem.Data = await GetMonitorSystem.Address.PostJsonAsync(req).ReceiveString();

        }

    }

}