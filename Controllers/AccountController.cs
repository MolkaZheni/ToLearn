using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using LearnApp.Models;
using EllipticCurve;

namespace LearnApp.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            using (ourDBcontext db = new ourDBcontext())
            {
                return View(db.Useraccount.ToList());
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(useraccount account)
        {
            if (ModelState.IsValid)
            {
                using (ourDBcontext db = new ourDBcontext())
                {

                    db.Useraccount.Add(account);
                    db.SaveChanges();

                }
                ModelState.Clear();
                ViewBag.Message = account.UserName +" " + "successfully registred.";

                
            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(useraccount user)
        {
                using (ourDBcontext db = new ourDBcontext())
                {

                    var usr = db.Useraccount.Single(u => u.UserName == user.UserName && u.Password == user.Password);
                    if (usr != null)
                    {
                        Session["UserId"] = usr.UserId.ToString();
                        Session["UserName"] = usr.UserName.ToString();
                        return RedirectToAction("LoggedIn");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username or Password is wrong. ");
                    }


                }   
            return View();
        }
        public ActionResult LoggedIn()
        {

            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("connect");
            }
        }
    }
        
}
