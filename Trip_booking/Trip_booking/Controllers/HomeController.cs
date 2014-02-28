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

        // GET: /Student/
        public ActionResult Guests()
        {
            //return View(_ctx.Guest);
            return View(_ctx.Guest);
        }

        // GET: /Student/
        public ActionResult Legs()
        {
            //return View(_ctx.Guest);
            return View(_ctx.Legs);
        }

        // GET: /Student/
        public ActionResult LegsGuests()
        {
            //return View(_ctx.Guest);
            return View(_ctx.legs_guest);
        }

        // GET: /Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Trip_Name,LegID,Start_Date,Finish_Date")] Trips trips)
        {
            if (ModelState.IsValid)
            {
                _ctx.Trips.Add(trips);
                //_ctx.Entry(trips).State = EntityState.Added;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trips);
        }

        // GET: /Student/Create
        public ActionResult CreateGuest()
        {
            return View();
        }

        // POST: /Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGuest([Bind(Include = "Guest_Name,LegID,Guests_GuestID")] Guest guest)
        {
            if (ModelState.IsValid)
            {
                _ctx.Guest.Add(guest);
                //_ctx.Entry(trips).State = EntityState.Added;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(guest);
        }

        // GET: /Student/Create
        public ActionResult CreateLegs()
        {
            return View();
        }

        // POST: /Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLegs([Bind(Include = "Start_location,Finish_location,Start_Date,Finish_Date,TripID")] Legs legs)
        {
            if (ModelState.IsValid)
            {
                _ctx.Legs.Add(legs);
                //_ctx.Entry(trips).State = EntityState.Added;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(legs);
        }

        // GET: /Student/Create
        public ActionResult CreateLegsGuest()
        {
            return View();
        }

        // POST: /Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLegsGuest([Bind(Include = "LegID,GuestID")] legs_guest legsguest)
        {
            if (ModelState.IsValid)
            {
                _ctx.legs_guest.Add(legsguest);
                //_ctx.Entry(trips).State = EntityState.Added;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(legsguest);
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTrip([Bind(Include = "Trip_Name,LegID,Start_Date,Finish_Date")] Trips trips)
        {
            if (ModelState.IsValid)
            {
                _ctx.Trips.Add(trips);
                //_ctx.Entry(Guest).State = EntityState.Added;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            // if not valid, re-send View with already entered data
            return View(trips);
        }*/

       
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
