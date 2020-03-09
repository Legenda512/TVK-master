using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TVK.Web.Models;
using Flurl.Http;
using System.Threading;
using TVK.Client.Daemon.Web.Models;
using Microsoft.EntityFrameworkCore.Internal;

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

            string[] info = GetMonitorSystem.Data.Split(new char[] { ' ', '\n', '\r', ';', ':' , ',', '\"' }, StringSplitOptions.RemoveEmptyEntries) ;

            string subString = "LoadPercentage";

            int indexOfSubstring = info.IndexOf(subString);
            GetMonitorSystem.LoadPercentage = info[indexOfSubstring + 1];

            subString = "NumberOfCores";
            indexOfSubstring = info.IndexOf(subString);
            GetMonitorSystem.NumberOfCores = info[indexOfSubstring + 1];

            subString = "NumberOfLogicalProcessors";
            indexOfSubstring = info.IndexOf(subString);
            GetMonitorSystem.NumberOfLogicalProcessors = info[indexOfSubstring + 1];

            return View("Index", GetMonitorSystem);



        }

    }

}