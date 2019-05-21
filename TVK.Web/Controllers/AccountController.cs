using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TVK.Web.ViewModels; // пространство имен моделей RegisterModel и LoginModel
using TVK.Web.Models; // пространство имен UserContext и класса User
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace TVK.Web.Controllers
{
    
    public class AccountController : Controller
    {
        private testSQLContext db;
        public AccountController(testSQLContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Users user = await db.Users
                    .Include(u => u.Roles)
                        .ThenInclude(u => u.IdBackgroundroleNavigation)
                    .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Users user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    user = new Users { Nameusers = model.Nameusers, Email = model.Email, Password = model.Password };

                    Roles userrole = new Roles { IdUsersRole = user.IdUser, IdBackgroundrole = 2 };
                    user.Roles.Add(userrole);
                    db.Users.Add(user);
                    //db.Roles.Add(userrole);
                    await db.SaveChangesAsync();

                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Index", "Home");

                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        // персональная информация
        [HttpGet]
        public IActionResult PersonalInfromationuser()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PersonalInfromationuser(PersonalInformationUserModel personalmodel)
        {
            if (User.Identity.IsAuthenticated)
            {
                Users user = db.Users
                    .Where(t => t.Email == User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value)
                    .FirstOrDefault();
                if (user != null)
                {
                    PersonalInformation personaluser = new PersonalInformation
                    {
                        IdUserPersonalInformation = user.IdUser,
                        Gender = personalmodel.Gender,
                        Age = personalmodel.Age,
                        Lastname = personalmodel.Lastname,
                        Firstname = personalmodel.Firstname,
                        Secondname = personalmodel.Secondname
                    };
                    user.PersonalInformation.Add(personaluser);
                    await db.SaveChangesAsync();

                    return RedirectToAction("Profile", "Home");
                }

            }
            else
                ModelState.AddModelError("", "Не удалось запомнить персональную информацию о пользователе.");

            return View(personalmodel);
        }


            //if (ModelState.IsValid)
            //{
            //    Users user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            //    if (user == null)
            //    {
            //        PersonalInformation personaluser = new PersonalInformation { IdUserPersonalInformation = user.IdUser, Gender = personalmodel.Gender, Age = personalmodel.Age,
            //                                                                    Lastname = personalmodel.Lastname, Firstname = personalmodel.Firstname, Secondname = personalmodel.Secondname };
            //        user.PersonalInformation.Add(personaluser);
            //        //db.Roles.Add(userrole);
            //        await db.SaveChangesAsync();

            //        await Authenticate(user); // аутентификация

            //        return RedirectToAction("Index", "Home");

            //    }
            //    else
            //        ModelState.AddModelError("", "Не удалось запомнить персональную информацию о пользователе.");
            //}
            //return View(personalmodel);
        

        // дополнительная уомнактная информация информация
        [HttpGet]
        public IActionResult ContactInformmationuser()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactInformmationuser(ContactInformmationModel contactmodel)
        {
            if (User.Identity.IsAuthenticated)
            {
                Users user = db.Users
                    .Where(t => t.Email == User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value)
                    .FirstOrDefault();
                if (user != null)
                {
                    ContactInformation concactuser = new ContactInformation
                    {
                        IdUserContactInformation = user.IdUser,
                        PhoneOrEmail = contactmodel.PhoneOrEmail,
                        Comment = contactmodel.Comment

                    };


                    user.ContactInformation.Add(concactuser);
                    await db.SaveChangesAsync();

                    return RedirectToAction("Profile", "Home");
                }

            }
            else
                ModelState.AddModelError("", "Не удалось запомнить контактную информацию пользователя.");

            return View(contactmodel);
        }



        private async Task Authenticate(Users user)
        {
            Users userrole = db.Users
            .Include(t => t.Roles)
                .ThenInclude(t => t.IdBackgroundroleNavigation)
            .Where(t => t.IdUser == user.IdUser)
            .FirstOrDefault();
            string idRole = "";
            foreach (var p in userrole.Roles)
            {
                idRole = p.IdBackgroundrole.ToString();
            }

            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, idRole)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

    }
    
}