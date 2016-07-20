using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceBackEnd;

namespace WebsiteFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var basketList = DatabaseAccess.GetBasket("1");
            ViewBag.Listing = basketList;
            return View();
        }

        public ActionResult Add1ToCart()
        {
            var item1 = DatabaseAccess.GetItem("Item1");
            DatabaseAccess.AddItem(item1);

            ViewBag.Message = "Item has been added.";

            return View();
        }

        public ActionResult Add2ToCart()
        {
            var item2 = DatabaseAccess.GetItem("Item2");
            DatabaseAccess.AddItem(item2);

            ViewBag.Message = "Item has been added.";

            return View();
        }

        public ActionResult ClearCart()
        {
            DatabaseAccess.RemoveItems("1");

            ViewBag.Message = "Items have been cleared.";

            return View();
        }
    }
}