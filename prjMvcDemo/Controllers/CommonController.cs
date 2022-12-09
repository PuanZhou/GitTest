using prjMvcDemo.Models;
using prjMvcDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult Home()
        {
            CCustomer user = Session[CDictionary.SK_LOGINED_USER]as CCustomer;
            if(user == null)
                return RedirectToAction("Login");
            return View(user);
        }        
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(CLoginViewModel vModel)
        {
            CCustomer cust = (new CCustomerFactory()).queryByEmail(vModel.txtAccount);
            if (cust.fPassword.Equals(vModel.txtPassword))
            {
                Session[CDictionary.SK_LOGINED_USER] = cust;
                return RedirectToAction("Home");
            }
            return View();
        }
    }
}