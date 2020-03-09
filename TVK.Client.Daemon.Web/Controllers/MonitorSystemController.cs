using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using TVK.Client.Daemon.Web.Models;

namespace TVK.Client.Daemon.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitorSystemController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(GetMonitorSystem GetMonitorSystem)
        {
            GetMonitorSystem.Processor(GetMonitorSystem);

            return Ok(GetMonitorSystem); //попробовать вывести в консоль или файл

        }

    }
}