using prjMvcDemo.Models;
using prjMvcDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class ShoppingController : Controller
    {        
        public ActionResult CartView()
        {
            List<CShoppingCartItem> cart = Session[CDictionary.SK_已經購買的_商品們_列表] as List<CShoppingCartItem>;
            if (cart == null)
                return RedirectToAction("List");
            return View(cart);
        }
        // GET: Shopping
        public ActionResult List()
        {
            IEnumerable<tProduct> datas = from t in (new dbDemoEntities()).tProduct
                        select t; 
            return View(datas);
        }
        public ActionResult AddToSession(int? id)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProduct prod = db.tProduct.FirstOrDefault(t => t.fId == id);
            if (prod == null)
                return RedirectToAction("List");
            return View(prod);
        }
        [HttpPost]
        public ActionResult AddToSession(CAddToCartViewModel vModel)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProduct prod = db.tProduct.FirstOrDefault(t => t.fId == vModel.txtFid);
            if (prod == null)
                return RedirectToAction("List");

           List<CShoppingCartItem> list =  Session[CDictionary.SK_已經購買的_商品們_列表] as List<CShoppingCartItem>;
           if (list == null)
            {
                list = new List<CShoppingCartItem>();
                Session[CDictionary.SK_已經購買的_商品們_列表] = list;
            }
            CShoppingCartItem item = new CShoppingCartItem() { 
                count= vModel.txtCount,
                price=(decimal) prod.fPrice,
                productId=vModel.txtFid,
                product= prod
            };
            list.Add(item);
             
            return RedirectToAction("List");
        }


        public ActionResult AddToCart(int? id)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProduct prod = db.tProduct.FirstOrDefault(t => t.fId == id);
            if (prod == null)
                return RedirectToAction("List");
            return View(prod);
        }
        [HttpPost]
        public ActionResult AddToCart(CAddToCartViewModel vModel)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProduct prod = db.tProduct.FirstOrDefault(t => t.fId == vModel.txtFid);
            if (prod == null)
                return RedirectToAction("List");

            tShoppingCart item = new tShoppingCart();
            item.fCount = vModel.txtCount;
            item.fCustomerId = 1;
            item.fDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            item.fPrice = prod.fPrice;
            item.fProductId = vModel.txtFid;

            db.tShoppingCart.Add(item);
            db.SaveChanges();
            return RedirectToAction("List");
        }
    }
}