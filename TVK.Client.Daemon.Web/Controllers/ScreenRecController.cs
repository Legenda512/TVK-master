using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TVK.Client.Daemon.Web.Models;
using System.Runtime.InteropServices;
using System.Threading;

namespace TVK.Client.Daemon.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenRecController : ControllerBase
    {


        [HttpPost]
        public async Task<IActionResult> Post(ScreenRecorder ScreenRecorder)
        {

            
            using (HttpClient client = new HttpClient())
            {
                var a = client.GetAsync("http://localhost:54278/home/" + ScreenRecorder.Command).Result;
                Console.Write(a);
            }
                
            return Ok();
        }


    }
}