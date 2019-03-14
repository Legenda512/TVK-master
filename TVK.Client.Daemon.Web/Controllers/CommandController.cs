using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliWrap;
using Microsoft.AspNetCore.Mvc;
using TVK.Client.Daemon.Web.Models;

namespace TVK.Client.Daemon.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(Request request)
        {

            var result = await Cli.Wrap(request.Command).ExecuteAsync();
            //HttpContext.Response.Body.Write(Encoding.UTF8.GetBytes("result"));
            return Ok(result.StandardOutput); //попробовать вывести в консоль или файл

            

        }
    }
}
