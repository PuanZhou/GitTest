using prjMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult List()
        {
            string keyword = Request.Form["txtKeyword"];
            CCustomerFactory f = new CCustomerFactory();
            List<CCustomer> list = null;
            if(string.IsNullOrEmpty(keyword))
                list = f.queryAll();
            else
                list = f.queryByKeyword(keyword);
            return View(list);
        }

        // GET: Customer
        public ActionResult New()
        {
            return View();
        }
        // GET: Customer
        public ActionResult Save()
        {
            CCustomer x = new CCustomer();
            x.fName = Request.Form["txtName"];
            x.fPhone = Request.Form["txtPhone"];
            x.fEmail = Request.Form["txtEmail"];
            x.fAddress = Request.Form["txtAddress"];
            x.fPassword = Request.Form["txtPassword"];

            (new CCustomerFactory()).insert(x);
            return RedirectToAction("List");
        }
        public ActionResult Delete(int? id)
        {            
            (new CCustomerFactory()).delete((int)id);
            return RedirectToAction("List");
        }
        public ActionResult Edit(int? id)
        {
            CCustomer x=(new CCustomerFactory()).queryById((int)id);
            return View(x);
        }
        [HttpPost]
        public ActionResult Edit(CCustomer x )
        {
            (new CCustomerFactory()).update(x);
            return RedirectToAction("List");
        }
    }
}