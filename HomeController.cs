using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMS.db;
using SMS.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace SMS.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            SMSContext database = new SMSContext();

            List<mymodel> mm = new List<mymodel>();

            var res= database.Infos.ToList();

            foreach (var item in res)
            {
                mm.Add(new mymodel
                {
                    Id = item.Id,
                    Sname = item.Sname,
                    Class = item.Class, 
                    ClassTeacher = item.ClassTeacher,


                });


            }

            return View(mm);
        }


        [HttpGet]
        public IActionResult Log()
        {
            

            return View();  
        }

        [HttpPost]
        public IActionResult Log(logmodel obj)
        {
            SMSContext database = new SMSContext();

            var res = database.Loggs.Where(i => i.AdminId == obj.AdminId).FirstOrDefault();

            if (res == null)
            {

                TempData["Invalid"] ="Admin-Id Is Invalid";
            }
            else
            {
                if(res.AdminId==obj.AdminId && res.Pass == obj.Pass)
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, res.Pass), };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };
                    HttpContext.SignInAsync(
                     CookieAuthenticationDefaults.AuthenticationScheme,
                     new ClaimsPrincipal(identity),
                   authProperties);


                   

                    return RedirectToAction("Index", "Home");

                }
                else
                {

                    ViewBag.Inv = "Wrong Password";

                    return View("Log");
                }
            }

            return View("Log");
        }

        [HttpGet]
        public IActionResult Add()
        {
          
            return View();
        }

        [HttpPost]
        public IActionResult Add(mymodel obj)
        {
            SMSContext database = new SMSContext();

            Info tt = new Info();

            tt.Id = obj.Id;
            tt.Sname = obj.Sname;
            tt.Roll=obj.Roll;
            tt.Class = obj.Class;
            tt.Age = obj.Age;
            tt.Section = obj.Section;
            tt.ClassTeacher = obj.ClassTeacher;
            tt.Principal = obj.Principal;
            tt.BloodGroup = obj.BloodGroup; 
            tt.HouseDress = obj.HouseDress;
            tt.Details = obj.Details;   
            tt.Performance = obj.Performance;
            tt.Doa = obj.Doa;
            tt.CurrentFee = obj.CurrentFee; 
            tt.PhoneNumber = obj.PhoneNumber;   
            tt.Email = obj.Email;   
            tt.Dob = obj.Dob;
            tt.Address = obj.Address;   
            tt.AdharNo = obj.AdharNo;   

            if (obj.Id == 0)
            {
                database.Infos.Add(tt);
                database.SaveChanges();
            }
            else
            {
                database.Entry(tt).State = EntityState.Modified;
                database.SaveChanges();
            }

            return RedirectToAction("Index","Home");
        }

        public IActionResult Del(int id)
        {
            SMSContext database = new SMSContext();

            var de = database.Infos.Where(i => i.Id == id).FirstOrDefault();
            database.Infos.Remove(de);
            database.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult edit(int id)
        {
            SMSContext database = new SMSContext();

            mymodel tt = new mymodel();

            var editing = database.Infos.Where(e => e.Id == id).FirstOrDefault();
         
            tt.Id = editing.Id;
            tt.Sname = editing.Sname;
            tt.Roll=editing.Roll;
            tt.Class = editing.Class;
            tt.Age = editing.Age;
            tt.Section = editing.Section;
            tt.ClassTeacher = editing.ClassTeacher; 
            tt.Principal = editing.Principal;
            tt.BloodGroup = editing.BloodGroup;
            tt.HouseDress = editing.HouseDress; 
            tt.Details = editing.Details;
            tt.Performance = editing.Performance;
            tt.Doa = editing.Doa;
            tt.CurrentFee = editing.CurrentFee; 
            tt.PhoneNumber = editing.PhoneNumber;
            tt.Email = editing.Email;   
            tt.Dob = editing.Dob;   
            tt.Address = editing.Address;   
            tt.AdharNo = editing.AdharNo;



            return View("Add", tt);   //<method>, <obj of the model>
        }

        //details=====================================


        public IActionResult Details(int id)
        {
            SMSContext database = new SMSContext();

            mymodel tt = new mymodel();

            var editing = database.Infos.Where(e => e.Id == id).FirstOrDefault();

            tt.Id = editing.Id;
            tt.Sname = editing.Sname;
            tt.Roll = editing.Roll;
            tt.Class = editing.Class;
            tt.Age = editing.Age;
            tt.Section = editing.Section;
            tt.ClassTeacher = editing.ClassTeacher;
            tt.Principal = editing.Principal;
            tt.BloodGroup = editing.BloodGroup;
            tt.HouseDress = editing.HouseDress;
            tt.Details = editing.Details;
            tt.Performance = editing.Performance;
            tt.Doa = editing.Doa;
            tt.CurrentFee = editing.CurrentFee;
            tt.PhoneNumber = editing.PhoneNumber;
            tt.Email = editing.Email;
            tt.Dob = editing.Dob;
            tt.Address = editing.Address;   
            tt.AdharNo= editing.AdharNo;


            return View("Details", tt);   //<method>, <obj of the model>
        }



        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return View("Log");
        }



    }
}