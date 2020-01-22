using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TVK.Web.Controllers
{
    public class RegisterIPController : Controller
    {
        public static List<string> computer_IP = new List<string>();
        public IActionResult Index(string IP)
        {
            computer_IP.Add(IP);
            return Ok();
        }
    }
}