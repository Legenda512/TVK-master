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
    public class ScreenRecorderController : Controller
    {
        static bool thread = false;
        Thread mythread = new Thread(GetPicture.Get_Picture);
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

            if (ScreenRecorder.Command == "start" && thread == false)
            {
                thread = true;
                mythread.IsBackground = true;
                mythread.Start();
            }


            ScreenRecorder.Data = await ScreenRecorder.Address.PostJsonAsync(ScreenRecorder).ReceiveString();

            

            //if (ScreenRecorder.Command == "stop" && thread == true)
            //{
            //    mythread.
            //}



            return View("Index", ScreenRecorder);
        }



    }
}