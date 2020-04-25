using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TVK.Web.Controllers
{
    public class RegisterIPController : Controller
    {
        private testSQLContext db;
        public RegisterIPController(testSQLContext context)
        {
            db = context;
        }

        public static List<string> computer_IP = new List<string>();
        public IActionResult Index(string IP)
        {
            IP += ":5000";
            Pc pc = new Pc();
            
            pc = db.Pc.Where(x => x.IpAddress == IP).FirstOrDefault();
 
            if (pc == null)
            {
                pc = new Pc { IpAddress = IP, NamePc = "1", IdOsPc = 1 };
                db.Pc.Add(pc);
                db.SaveChanges();
            }


            computer_IP.Add(IP);
            return Ok();
        }
    }
}
