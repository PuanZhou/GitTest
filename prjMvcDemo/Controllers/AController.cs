using prjLottoApp.Models;
using prjMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class AController : Controller
    {
        public ActionResult demoFileUpload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult demoFileUpload(HttpPostedFileBase photo)
        {
            photo.SaveAs(@"C:\QNote\Codes\slnMvcDemo\prjMvcDemo\Images\test.jpg");
            return View();
        }

        static int count = 0;
        public ActionResult showCountByCookie()
        {
            int count=0;
            HttpCookie x = Request.Cookies["KK"];
            if (x != null)
                count = Convert.ToInt32( x.Value);
            count++;
            x = new HttpCookie("KK");
            x.Value = count.ToString();
            x.Expires = DateTime.Now.AddSeconds(20);
            Response.Cookies.Add(x);

            ViewBag.COUNT = count;
            return View();
        }
        public ActionResult showCountBySession()
        {
            int count = 0;
            if (Session["KK"] != null)
                count = (int)Session["KK"];
            count++;
            Session["KK"] = count;
            ViewBag.COUNT = count;
            return View();
        }
        public ActionResult showCount()
        {
            count++;
            ViewBag.COUNT = count;
            return View();
        }



        public string demoResponse()
        {
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.Filter.Close();
            Response.WriteFile(@"C:\qnote\clock.jpg");
            Response.End();
            return "";
        }
        public string queryById(int? id)
        {
            if (id == null)
                return "找不到該客戶資料";

            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fId=" + id.ToString(), con);
            SqlDataReader reader = cmd.ExecuteReader();

            string s = "找不到該客戶資料";
            if (reader.Read())
                s = reader["fName"].ToString() + "<br/>" +
                    reader["fPhone"].ToString() + " / " + reader["fEmail"].ToString();
            con.Close();
            return s;

        }

        public string demoParameter(int? id)
        {

            if (id == 1)
                return "XBox 加入購物車成功";
            else if (id == 2)
                return "PS5 加入購物車成功";
            else if (id == 3)
                return "Switch 加入購物車成功";
            return "找不到該產品資料";
        }


        public string demoRequest()
        {
            string id = Request.QueryString["productId"];
            if (id == "1")
                return "XBox 加入購物車成功";
            else if (id == "2")
                return "PS5 加入購物車成功";
            else if (id == "2")
                return "Switch 加入購物車成功";
            return "找不到該產品資料";
        }
        public string demoServer()
        {
            return "目前伺服器上的實體位置：" + Server.MapPath(".");
        }

        public string sayHello()
        {
            return "Hello ASP.NET MVC";
        }
        [NonAction]
        public string lotto()
        {
            return (new CLottoGen()).getLotto();
        }
        // GET: A
        public ActionResult bindingById(int? id)
        {
            CCustomer x = null;
            if (id != null)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fId=" + id.ToString(), con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    x = new CCustomer();
                    x.fId = (int)reader["fId"];
                    x.fName = reader["fName"].ToString();
                    x.fPhone = reader["fPhone"].ToString();
                    x.fEmail = reader["fEmail"].ToString();

                }
                con.Close();
            }
            return View(x);
        }
        public string testingDelete(int? id)
        {
            if (id == null)
                return "沒有指定PK";
            (new CCustomerFactory()).delete((int)id);
            return "刪除資料成功";

        }

        public string testingQuery()
        {
            return "目前客戶數：" + (new CCustomerFactory()).queryAll().Count.ToString();
        }

        public string testingUpdate()
        {
            CCustomer x = new CCustomer()
            {
                fId = 5,
                fAddress = "Taipei",
                fPhone = "0911223445"
            };
            (new CCustomerFactory()).update(x);
            return "修改資料成功";
        }
        public string testingInsert()
        {
            CCustomer x = new CCustomer()
            {
                fName = "Clock Chen",
                // fAddress = "Taipei",
                fEmail = "clock@cdc.gov.tw",
                fPassword = "1922",
                //  fPhone = "0911223445"
            };
            (new CCustomerFactory()).insert(x);
            return "新增資料成功";
        }

        // GET: A
        public ActionResult demoForm()
        {
            ViewBag.Ans = "?";
            if (!string.IsNullOrEmpty(Request.Form["txtA"]))
            {
                double a = Convert.ToInt32(Request.Form["txtA"]);
                double b = Convert.ToInt32(Request.Form["txtB"]);
                double c = Convert.ToInt32(Request.Form["txtC"]);
                double r = b * b - 4 * a * c;
                r = Math.Sqrt(r);
                ViewBag.Ans = ((-b+r)/(2*a)).ToString("0.0#")+" Or X = "
                    +((-b - r) / (2 * a)).ToString();
                ViewBag.a = a;
                ViewBag.b = b;
                ViewBag.c = c;
            }
            return View();
        }


            public ActionResult showById(int? id)
        {
            if (id != null)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fId=" + id.ToString(), con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    CCustomer x = new CCustomer();
                    x.fId = (int)reader["fId"];
                    x.fName = reader["fName"].ToString();
                    x.fPhone = reader["fPhone"].ToString();
                    x.fEmail = reader["fEmail"].ToString();
                    ViewBag.KK = x;
                }
                con.Close();
            }
            return View();
        }
    }
}