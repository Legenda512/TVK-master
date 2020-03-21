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
        Thread mythreadGetFile = new Thread(GetPicture.Get_Picture);
        Thread mythreadDeleteFile = new Thread(GetPicture.Delet_Picture);
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Post(ScreenRecorder ScreenRecorder)
        {
            string ip_PC = ScreenRecorder.Address;
            ScreenRecorder.Address = "http://" + ip_PC + "/api/screenrec";

            if (ScreenRecorder == null)
            {
                throw new ArgumentNullException(nameof(ScreenRecorder));
            }

            if (ScreenRecorder.Command == "start" && thread == false)
            {
                thread = true;
                mythreadGetFile.IsBackground = true;
                mythreadGetFile.Start();
            }


            ScreenRecorder.Data = await ScreenRecorder.Address.PostJsonAsync(ScreenRecorder).ReceiveString();



            if (ScreenRecorder.Command == "stop" && thread == true)
            {
                thread = false;
                mythreadDeleteFile.IsBackground = true;
                mythreadDeleteFile.Start();

            }



            return View("Index", ScreenRecorder);
        }



    }
}