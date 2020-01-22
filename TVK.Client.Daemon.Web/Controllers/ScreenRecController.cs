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
        //private static string outputPath;
        //static ScreenRecorder screenRec;
        //[DllImport("user32.dll")]
        //static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);
        //static ScreenRecController()
        //{

        //    if (Directory.Exists(Environment.CurrentDirectory))
        //    {
        //        string pathName = Environment.CurrentDirectory + "\\Video";
        //        Directory.CreateDirectory(pathName);
        //        outputPath = pathName;
        //    }

        //    const int ENUM_CURRENT_SETTINGS = -1;

        //    DEVMODE devMode = default;
        //    devMode.dmSize = (short)Marshal.SizeOf(devMode);
        //    EnumDisplaySettings(null, ENUM_CURRENT_SETTINGS, ref devMode);

        //    screenRec = new ScreenRecorder(new Rectangle(0, 0, devMode.dmPanningWidth,
        //                                                    devMode.dmPanningHeight), outputPath);
        //}


        [HttpPost]
        public async Task<IActionResult> Post(string command)
        {

            
            using (HttpClient client = new HttpClient())
            {
                var a = client.GetAsync("http://localhost:54278/home/start").Result;
                Console.Write(a);
            }
                
            return Ok();
        }

        //private static void Work(CancellationToken cancelToken)
        //{
        //    while (true)
        //    {
        //        if (cancelToken.IsCancellationRequested)
        //        {
        //            screenRec.Stop();
        //            screenRec.cleanUp();
        //            return;
        //        }
        //        screenRec.RecordVideo();
        //        screenRec.RecordAudio();
        //    }
        //}


        //[StructLayout(LayoutKind.Sequential)]
        //struct DEVMODE
        //{
        //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        //    public string dmDeviceName;
        //    public short dmSpecVersion;
        //    public short dmDriverVersion;
        //    public short dmSize;
        //    public short dmDriverExtra;
        //    public int dmFields;
        //    public int dmPositionX;
        //    public int dmPositionY;
        //    public int dmDisplayOrientation;
        //    public int dmDisplayFixedOutput;
        //    public short dmColor;
        //    public short dmDuplex;
        //    public short dmYResolution;
        //    public short dmTTOption;
        //    public short dmCollate;
        //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        //    public string dmFormName;
        //    public short dmLogPixels;
        //    public int dmBitsPerPel;
        //    public int dmPelsWidth;
        //    public int dmPelsHeight;
        //    public int dmDisplayFlags;
        //    public int dmDisplayFrequency;
        //    public int dmICMMethod;
        //    public int dmICMIntent;
        //    public int dmMediaType;
        //    public int dmDitherType;
        //    public int dmReserved1;
        //    public int dmReserved2;
        //    public int dmPanningWidth;
        //    public int dmPanningHeight;
        //}
    }
}