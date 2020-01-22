using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TVK.Web.Models;
using TVK.Web.ViewModels;


namespace TVK.Web.Controllers
{
    public class ChartsController : Controller
    {
        private testSQLContext db;
        public ChartsController(testSQLContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult IndexCharts()
        {
            return View();
        }

        public IActionResult BarUserRoles()
        {
            List<Roles> roles = db.Roles.ToList();

            int admin = 0;
            int guest = 0;

            foreach (var role in roles)
            {
                if (role.IdBackgroundrole == 1) admin++;
                else guest++;
            }

            var lstModel = new List<SimpleReportViewModel>();
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Всего пользователей",
                Quantity = admin + guest
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Администраторы",
                Quantity = admin
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Гости",
                Quantity = guest
            });

            return View(lstModel);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(LoginModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Users user = await db.Users
        //            .Include(u => u.Roles)
        //                .ThenInclude(u => u.IdBackgroundroleNavigation)
        //            .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
        //        if (user != null)
        //        {
        //            await Authenticate(user); // аутентификация

        //            return RedirectToAction("Index", "Home");
        //        }
        //        ModelState.AddModelError("", "Некорректные логин и(или) пароль");
        //    }
        //    return View(model);
        //}

        [HttpGet("LineCommandData")]
        public IActionResult LineCommandData(string StartDate, string FinishDate)
        {
            DateTime finishDate;
            DateTime startDate;
            if (FinishDate == null || StartDate == null)
                return RedirectToAction("IndexCharts", "Charts");


            try
            {

                startDate = DateTime.Parse(StartDate);
                finishDate = DateTime.Parse(FinishDate);
                finishDate = finishDate.AddDays(1);
            }
            catch
            {
                return RedirectToAction("IndexCharts", "Charts");
            }

            

            List<Command> commands = db.Command.ToList();

            List<string> result = new List<string>();

            foreach (var command in commands)
            {
                if(command.Time >= startDate && command.Time <= finishDate)
                    result.Add(command.Time.ToShortDateString());
            }

            var resultnew = result.GroupBy(x => x).OrderByDescending(x => x.Count());
            var lstModel = new List<SimpleReportViewModel>();

            

            foreach (var time in resultnew)
            {
                lstModel.Add(new SimpleReportViewModel
                {
                    DimensionOne = time.Key,
                    Quantity = time.Count()
                });
            }
            var a = lstModel.OrderBy(x => DateTime.Parse(x.DimensionOne));
            ViewData["XLabels"] = Newtonsoft.Json.JsonConvert.SerializeObject(a.Select(x => x.DimensionOne).ToList());
            ViewData["YValues"] = Newtonsoft.Json.JsonConvert.SerializeObject(a.Select(x => x.Quantity).ToList());
            return View(a);
        }

        public IActionResult PieCommand()
        {
            List<Command> commands = db.Command.ToList();
            List<int> result = new List<int>();

            foreach (var command in commands)
            {
                result.Add(command.IdCommandBackgroundCommand);
            }

            var resultnew = result.GroupBy(x => x).OrderByDescending(x => x.Count());


            var lstModel = new List<SimpleReportViewModel>();

            foreach (var count in resultnew)
            {
                BackgroundCommand backgroundCommand = db.BackgroundCommand
                .Where(t => t.IdBackgroundCommand == count.Key)
                .FirstOrDefault();

                lstModel.Add(new SimpleReportViewModel
                {
                    DimensionOne = backgroundCommand.Command,
                    Quantity = count.Count()
                });
            }

            
            return View(lstModel);
        }

        public IActionResult BarUser()
        {
            List<Command> commands = db.Command.ToList();
            List<int> result = new List<int>();

            foreach (var command in commands)
            {
                result.Add(command.IdSender);
            }

            var resultnew = result.GroupBy(x => x).OrderByDescending(x => x.Count());


            var lstModel = new List<SimpleReportViewModel>();

            foreach (var count in resultnew)
            {
                Users user = db.Users
                .Where(t => t.IdUser == count.Key)
                .FirstOrDefault();

                lstModel.Add(new SimpleReportViewModel
                {
                    DimensionOne = user.Nameusers,
                    Quantity = count.Count()
                });
            }

            return View(lstModel);
        }
    }

}