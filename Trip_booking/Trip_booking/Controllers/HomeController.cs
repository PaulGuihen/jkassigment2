using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trip_booking.Models;
using Trip_booking.DAL;


namespace Trip_booking.Controllers
{
    public class HomeController : Controller
    {
        private GuestsContext _ctx = new GuestsContext();

        // GET: /Student/
        public ActionResult Index()
        {
            //return View(_ctx.Guest);
            return View(_ctx.Trips);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Guest_Name")] Guest Guest)
        {
            if (ModelState.IsValid)
            {
                _ctx.Guest.Add(Guest);
                
                //_ctx.Entry(Guest).State = EntityState.Added;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
                
            }
            
            // if not valid, re-send View with already entered data
            return View(Guest);
        }

       
        /*public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }*/

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your app description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}
