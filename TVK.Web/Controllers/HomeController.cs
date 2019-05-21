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
            //if (User.Identity.IsAuthenticated)
            //{
            //    string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            //    if (role.Equals("1"))
            //        role = "Администратор";
            //    else role = "Гость";
            //    return Content($"ваша роль: {role}");
            //}
            //return Content("не аутентифицирован");

            return View();
        }

        [Authorize(Roles = "1")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Post(TvkCommand tvkCommand)
        {

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
                .Where(t => t.IpAddress == tvkCommand.Address)
                .FirstOrDefault();

            if (pc == null)
            {
                pc = new Pc { IpAddress = tvkCommand.Address, NamePc = "1", IdOsPc = 1 };
                db.Pc.Add(pc);
                await db.SaveChangesAsync();
            }

            if (tvkCommand.Command == "hostname")
            {
                pc.NamePc = tvkCommand.Data;
                db.Pc.Update(pc);
                await db.SaveChangesAsync();
            };

            Systeminfo systeminfo = new Systeminfo { };
            if (tvkCommand.Command == "systeminfo")
            {
                string[] info = tvkCommand.Data.Split(new char[] { ' ','\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
               
                string subString = "узла:";
                int indexOfSubstring = info.IndexOf(subString);
                systeminfo.Nodename = info[indexOfSubstring + 1];
                //if (info[i] == "узла:")
                //    systeminfo.Nodename = info[i + 1];

                subString = "Название";
                string subString1 = "ОС:";
                indexOfSubstring = info.IndexOf(subString);
                int indexOfSubstring1 = info.IndexOf(subString1);
                if (indexOfSubstring1 - indexOfSubstring == 1)
                    systeminfo.NameOfOs = info[indexOfSubstring + 2] + " " + info[indexOfSubstring + 3] + " " + info[indexOfSubstring + 4] + " " + info[indexOfSubstring + 5];
                //if (info[i] == "Название" && info[i+1] == "ОС:")
                //    systeminfo.NameOfOs = info[i + 2] + " "+ info[i + 3] + " " + info[i + 4] + " " + info[i + 5];

                subString = "Изготовитель";
                indexOfSubstring = info.IndexOf(subString);
                if (info[indexOfSubstring + 1] == "ОС:")
                    systeminfo.SystemManufacturer = info[indexOfSubstring + 2] + " " + info[indexOfSubstring + 3];
                //if (info[i] == "Изготовитель" && info[i + 1] == "ОС:")
                //    systeminfo.SystemManufacturer = info[i + 2] + " " + info[i + 3];

                subString = "Модель";
                indexOfSubstring = info.IndexOf(subString);
                if (info[indexOfSubstring + 1] == "системы:")
                    systeminfo.ModelSystem = info[indexOfSubstring + 2];
                //if (info[i] == "Изготовитель" && info[i + 1] == "системы:")
                //    systeminfo.ModelSystem = info[i + 2];

                subString = "Тип";
                indexOfSubstring = info.IndexOf(subString);
                if (info[indexOfSubstring + 1] == "системы:")
                    systeminfo.TypeOfSystem = info[indexOfSubstring + 2] + " " + info[indexOfSubstring + 3];
                //if (info[i] == "Тип" && info[i + 1] == "системы:")
                //    systeminfo.TypeOfSystem = info[i + 2] + " " + info[i + 3];

                subString = "Полный";
                indexOfSubstring = info.IndexOf(subString);
                if (info[indexOfSubstring + 1] == "объем" && info[indexOfSubstring + 2] == "физической" && info[indexOfSubstring + 3] == "памяти:")
                    systeminfo.PhysicalMemory = info[indexOfSubstring + 4] + " " + info[indexOfSubstring + 5];
                //if (info[i] == "Полный" && info[i + 1] == "объем" && info[i + 2] == "физической" && info[i + 3] == "памяти:")
                //    systeminfo.PhysicalMemory = info[i + 4] + " " + info[i + 5];

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
                //if (info[i] == "Сетевые" && info[i + 1] == "адаптеры:")
                //{
                //    while(info[i] != "Требования")
                //        systeminfo.NetworkAdapters = info[i] + " ";
                //}      

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
                IdSender = 1,
                IdRecipient = user.IdUser,
                Time = DateTime.Now,
                IdCommandBackgroundCommand = backgroundCommand.IdBackgroundCommand,
                IdDataCommand = data.IdData,
                IdPcCommand = pc.IdPc
            };

            user.Command.Add(command);
            await db.SaveChangesAsync();
            return View("Index", tvkCommand);
            }



        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "1,2")]
        public IActionResult Profile()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
