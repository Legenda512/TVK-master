using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScreenRecorder.Models;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace ScreenRecorder.Controllers
{
    [Route("home")]
    public class homeController : Controller
    {
        private static string outputPath;
        static ScrRecorder screenRec;
        static CancellationTokenSource cancelSource = new CancellationTokenSource();
        static bool thread = false;

        static homeController()
        
        {
            if (Directory.Exists(Environment.CurrentDirectory))
            {
                string pathName = "C:\\Video";
                Directory.CreateDirectory(pathName);
                outputPath = pathName;
            }

            screenRec = new ScrRecorder(new Rectangle(0, 0, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Width,
                                                            System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Height), "C:\\");
        }


        public string Index()
        {
            return "";
        }
        [HttpGet]
        [Route("start")]
        public string start()
        {
            if (thread == false)
            {
                thread = true;
                new Thread(() =>
                {
                    try
                    {
                        Work(cancelSource.Token).Wait();
                    }
                    catch (OperationCanceledException)
                    {
                        Console.WriteLine("Canceled!");
                    }
                }).Start();
            }

            return "";
        }

        [HttpGet]
        [Route("stop")]
        public string stop()
        {
            cancelSource.Cancel();
            thread = false;


            return "";
        }


        private static async Task Work(CancellationToken cancelToken)
        {
            while (true)
            {

                if (cancelToken.IsCancellationRequested)
                {
                    await Task.Delay(5000).ContinueWith(_ =>
                    {
                        screenRec.Stop();
                        screenRec.cleanUp();
                    });
                    
                    return;
                }
                screenRec.RecordVideo();
                screenRec.RecordAudio();
            }
        }

    }
}
