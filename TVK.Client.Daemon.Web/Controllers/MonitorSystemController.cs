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


            //using (HttpClient client = new HttpClient())
            //{
            //    var a = client.GetAsync("http://localhost:54278/home/" + GetMonitorSystem.Command).Result.Content;
            //    Console.Write(a);
            //}

            //return Ok();

            var result =  GetMonitorSystem.Processor();
            // HttpContext.Response.Body.Write(Encoding.UTF8.GetBytes("result"));
            return Ok(result); //попробовать вывести в консоль или файл

        }

    }
}