using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trip_booking.Models;
using Trip_booking.DAL;


// home controller class
namespace Trip_booking.Controllers
{
    public class HomeController : Controller
    {
        //connection to data base
        private GuestsContext _ctx = new GuestsContext();

        // return trips home page 
        public ActionResult Index()
        {
            
            return View(_ctx.Trips);
        }

        // retrun guest data to view
        public ActionResult Guests()
        {
            //return View(_ctx.Guest);
            return View(_ctx.Guest);
        }

        // return legs data view
        public ActionResult Legs()
        {
            
            return View(_ctx.Legs);
        }

        // return legguest data to view
        public ActionResult LegsGuests()
        {
            //return View(_ctx.Guest);
            return View(_ctx.legs_guest);
        }

        // return create view form 
        public ActionResult Create()
        {
           
            return View();
        }



// send create information to the data base
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Trip_Name,LegID,Start_Date,Finish_Date")] Trips trips)
        {

            //validation 
            if (ModelState.IsValid)
            {
                _ctx.Trips.Add(trips);
                //_ctx.Entry(trips).State = EntityState.Added;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            // return to create trips view page if validation is false 
            return View(trips);
        }

        // create guest view 
        public ActionResult CreateGuest()
        {
            return View();
        }

        //method to check trip status

        public String GetStatus(int TripID)
        {
            int nLegCount = 0;
            List<Legs> legs = _ctx.Legs.ToList();

            
            foreach (var leg in legs)
            {

                if (TripID == leg.TripID)
                {
                    nLegCount++;
                }

            }
            // check to see if legs is greater than 1 
            if (nLegCount > 1)
            {
                return "viable";
            }
            else
                return "";

        }

        // Partial view functiion to get legs details using AJAX
        public PartialViewResult GetLegDetailsForTipe(int TripID)
        {
            List<Legs> legs = _ctx.Legs.ToList();

            bool foundLegs = false;
            foreach (var leg in legs)
            {
                // find all legs for this trip   n display result in partial page
                if (TripID == leg.TripID)
                {
                    ViewBag.Tiffin += "Leg Start " + leg.Start_location + " " + leg.Start_Date.ToShortDateString() + " Leg Finish " + leg.Finish_location + " " + leg.Finish_Date.ToShortDateString() + "<br>";
                    foundLegs = true;
                }

            }
            // if no legs added  display this 
            if (foundLegs==false)
            {
                ViewBag.Tiffin = "No Legs Added For This Trip";
            }

            //return partial page 
            return PartialView("_Result");
        }

        //create guest view 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGuest([Bind(Include = "Guest_Name,LegID,Guests_GuestID")] Guest guest)
        {
            if (ModelState.IsValid)
            {
                _ctx.Guest.Add(guest);
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

        
        // create legs form 
        public ActionResult CreateLegs()
        {
            // populate legs trips drop down list with data frm trips table
            PopulateTripsDropDownList();
            return View();
        }

     //create leg view with valiadtion 
        
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

            // if validation is successfull add data to DB
            if (ModelState.IsValid)
            {
                _ctx.Legs.Add(legs);  
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateTripsDropDownList();
            return View(legs);
        }

        // create legs guest 
        public ActionResult CreateLegsGuest()
        {
            return View();
        }

       // create legs guest form 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLegsGuest([Bind(Include = "LegID,GuestID")] legs_guest legsguest)
        {
            if (ModelState.IsValid)
            {
                _ctx.legs_guest.Add(legsguest);
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(legsguest);
        }

       
    }
}
