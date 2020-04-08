using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TVK.Web.Models;
using Flurl; // почитать
using Flurl.Http;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Internal;
using TVK.Client.Daemon.Web.Models;


namespace TVK.Web.Controllers
{

    public class HomeController : Controller
    {
        private testSQLContext db;
        public HomeController(testSQLContext context)
        {
            db = context;
        }

        [Authorize(Roles = "1,2")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("get_tvkcommand/{data_tvkcommand}")]
        public async Task<IActionResult> Post(string data_tvkcommand)
        {
            string[] data_tvkcommand_mas = data_tvkcommand.Split(' ');
            TvkCommand tvkCommand = new TvkCommand();
            tvkCommand.Address = data_tvkcommand_mas[0];
            tvkCommand.Command = data_tvkcommand_mas[1];

            string ip_PC = tvkCommand.Address;
            tvkCommand.Address = "http://" + ip_PC + "/api/command";

            DateTime start = new DateTime();
            start = DateTime.Now;

            Users user = new Users();
            if (User.Identity.IsAuthenticated)
            {
                 user = db.Users
                    .Where(t => t.Email == User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value)
                    .FirstOrDefault();
            }

            if (tvkCommand == null)
            {
                throw new ArgumentNullException(nameof(tvkCommand));
            }

            var req = new
            {
                command = tvkCommand.Command
            };
            tvkCommand.Data = await tvkCommand.Address.PostJsonAsync(req).ReceiveString();



            BackgroundCommand backgroundCommand = db.BackgroundCommand
                .Where(t => t.Command == tvkCommand.Command)
                .FirstOrDefault();

            Data data = new Data { LeadTime = DateTime.Now - start, Data1 = tvkCommand.Data };

            db.Data.Add(data);
            await db.SaveChangesAsync();

            Pc pc = db.Pc
                .Where(t => t.IpAddress == ip_PC)
                .FirstOrDefault();

            if (pc == null)
            {
                pc = new Pc { IpAddress = ip_PC, NamePc = "1", IdOsPc = 1 };
                db.Pc.Add(pc);
                await db.SaveChangesAsync();
            }

            if (tvkCommand.Command == "hostname")
            {
                pc.NamePc = tvkCommand.Data;
                db.Pc.Update(pc);
                await db.SaveChangesAsync();
            };

            Systeminfo systeminfo = new Systeminfo();
            if (tvkCommand.Command == "systeminfo")
            {
                string[] info = tvkCommand.Data.Split(new char[] { ' ','\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
               
                string subString = "узла:";
                int indexOfSubstring = info.IndexOf(subString);
                systeminfo.Nodename = info[indexOfSubstring + 1];


                subString = "Название";
                string subString1 = "ОС:";
                indexOfSubstring = info.IndexOf(subString);
                int indexOfSubstring1 = info.IndexOf(subString1);
                if (indexOfSubstring1 - indexOfSubstring == 1)
                    systeminfo.NameOfOs = info[indexOfSubstring + 2] + " " + info[indexOfSubstring + 3] + " " + info[indexOfSubstring + 4] + " " + info[indexOfSubstring + 5];


                subString = "Изготовитель";
                indexOfSubstring = info.IndexOf(subString);
                if (info[indexOfSubstring + 1] == "ОС:")
                    systeminfo.SystemManufacturer = info[indexOfSubstring + 2] + " " + info[indexOfSubstring + 3];


                subString = "Модель";
                indexOfSubstring = info.IndexOf(subString);
                if (info[indexOfSubstring + 1] == "системы:")
                    systeminfo.ModelSystem = info[indexOfSubstring + 2];


                subString = "Тип";
                indexOfSubstring = info.IndexOf(subString);
                if (info[indexOfSubstring + 1] == "системы:")
                    systeminfo.TypeOfSystem = info[indexOfSubstring + 2] + " " + info[indexOfSubstring + 3];


                subString = "Полный";
                indexOfSubstring = info.IndexOf(subString);
                if (info[indexOfSubstring + 1] == "объем" && info[indexOfSubstring + 2] == "физической" && info[indexOfSubstring + 3] == "памяти:")
                    systeminfo.PhysicalMemory = info[indexOfSubstring + 4] + " " + info[indexOfSubstring + 5];


                subString = "Сетевые";
                indexOfSubstring = info.IndexOf(subString);
                string subString2 = "Hyper-V:";
                int indexOfSubstring2 = info.IndexOf(subString2);
                if (info[indexOfSubstring + 1] == "адаптеры:")
                {
                    systeminfo.NetworkAdapters = "";
                    for (int i = indexOfSubstring + 2; i != indexOfSubstring2 - 1; i++ )
                        systeminfo.NetworkAdapters += info[i] + " ";
                }
  

                db.Systeminfo.Add(systeminfo);
                await db.SaveChangesAsync();
            };

            if (systeminfo.ModelSystem != null)
            {
                HistorySysteminfo historySysteminfo = new HistorySysteminfo
                {
                    DateHistory = DateTime.Now,
                    IdSysteminfo = systeminfo.IdSysteminfo,
                    IdPcHistorySysteminfo = pc.IdPc
                };
                db.HistorySysteminfo.Add(historySysteminfo);
                await db.SaveChangesAsync();
            }

            Command command = new Command
            {
                IdSender = user.IdUser,
                IdRecipient = user.IdUser,
                Time = DateTime.Now,
                IdCommandBackgroundCommand = backgroundCommand.IdBackgroundCommand,
                IdDataCommand = data.IdData,
                IdPcCommand = pc.IdPc
            };

            user.Command.Add(command);
            await db.SaveChangesAsync();
            return new JsonResult(tvkCommand);
        }



        [Authorize(Roles = "1,2")]
        public IActionResult Profile()
        {
            return View();
        }

        [Route("get_systeminfo/{address}")]
        public async Task<IActionResult> Get_systeminfo(string address)
        {
            GetMonitorSystem GetMonitorSystem = new GetMonitorSystem();

            GetMonitorSystem.Address = "http://" + address + "/api/monitorsystem";

            GetMonitorSystem.Data = await GetMonitorSystem.Address.PostJsonAsync(GetMonitorSystem).ReceiveString();

            string[] info = GetMonitorSystem.Data.Split(new char[] { ' ', '\n', '\r', ';', ':', ',', '\"' }, StringSplitOptions.RemoveEmptyEntries);

            string subString = "LoadPercentage";

            int indexOfSubstring = info.IndexOf(subString);
            GetMonitorSystem.LoadPercentage = info[indexOfSubstring + 1];

            subString = "NumberOfCores";
            indexOfSubstring = info.IndexOf(subString);
            GetMonitorSystem.NumberOfCores = info[indexOfSubstring + 1];

            subString = "NumberOfLogicalProcessors";
            indexOfSubstring = info.IndexOf(subString);
            GetMonitorSystem.NumberOfLogicalProcessors = info[indexOfSubstring + 1];

            subString = "TotalVisibleMemorySize";
            indexOfSubstring = info.IndexOf(subString);
            int TotalVisibleMemorySize = Int32.Parse(info[indexOfSubstring + 1]) / 1024;
            GetMonitorSystem.TotalVisibleMemorySize = TotalVisibleMemorySize.ToString();

            subString = "FreePhysicalMemory";
            indexOfSubstring = info.IndexOf(subString);
            int FreePhysicalMemory = Int32.Parse(info[indexOfSubstring + 1]) / 1024;
            GetMonitorSystem.FreePhysicalMemory = FreePhysicalMemory.ToString();

            Users user = new Users();
            if (User.Identity.IsAuthenticated)
            {
                user = db.Users
                   .Where(t => t.Email == User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value)
                   .FirstOrDefault();
            }


            MonitorSystem monitorSystem = new MonitorSystem();
            monitorSystem.IdSender = user.IdUser;
            monitorSystem.Address = address;
            monitorSystem.Data = GetMonitorSystem.Data;
            monitorSystem.Loadpercentage = GetMonitorSystem.LoadPercentage;
            monitorSystem.Numberofcores = GetMonitorSystem.NumberOfCores;
            monitorSystem.Numberoflogicalprocessors = GetMonitorSystem.NumberOfLogicalProcessors;
            monitorSystem.Totalvisiblememorysize = GetMonitorSystem.TotalVisibleMemorySize;
            monitorSystem.Freephysicalmemory = GetMonitorSystem.FreePhysicalMemory;
            monitorSystem.DateMonitorSystem = DateTime.Now;

            db.MonitorSystem.Add(monitorSystem);
            await db.SaveChangesAsync();



            return new JsonResult(GetMonitorSystem);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
