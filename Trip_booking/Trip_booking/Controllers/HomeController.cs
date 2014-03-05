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
        // populate legs trips drop down list with data frm trips table
        private void PopulateTripsDropDownList(object selectedTeams = null)
        {
            var tripsQuery = from d in _ctx.Trips
                             orderby d.Trip_Name
                             select d;
            ViewBag.TripID = new SelectList(tripsQuery, "TripID", "Trip_Name", selectedTeams);
        }

        
        // GET: /trips/Create
        public ActionResult CreateLegs()
        {
            PopulateTripsDropDownList();
            return View();
        }

        // POST: /Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLegs([Bind(Include = "Start_location,Finish_location,Start_Date,Finish_Date,TripID")] Legs legs)
        {
           // Using the legs.TirpID get the matching trip from the Trips table
            int nLegTripID = legs.TripID;
            var TripQyery = from t in _ctx.Trips where t.TripID == nLegTripID select t;

            // Validate the Legs start and finish date agains the Trip start and Finish date
            foreach (var trip in TripQyery)
            {
                DateTime tripStartDate = trip.Start_Date;
                DateTime tripEndDate = trip.Finish_Date;

                // Legs start date must be greater then Trips.StartDate
                if (tripStartDate > legs.Start_Date)
                {
                    ModelState.AddModelError("Start_Date", "Leg Start Date must not start before: " + tripStartDate.ToString());
                }

                //  Leg start date must not be after the trip end date
                if (tripEndDate < legs.Start_Date)
                {
                    ModelState.AddModelError("Start_Date", "Leg Start Date must not end after: " + tripEndDate.ToString());
                }

                // leg finish date must not be after the trip end date
                if (tripEndDate < legs.Finish_Date)
                {
                    ModelState.AddModelError("Finish_Date", "Leg Finish Date must not end after: " + tripEndDate.ToString());

                }

                // leg finish date canno be before trip start date
                if (tripStartDate > legs.Finish_Date)
                {
                    ModelState.AddModelError("Finish_Date", "Leg Finish Date must not end before: " + tripStartDate.ToString());

                }
            }

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
