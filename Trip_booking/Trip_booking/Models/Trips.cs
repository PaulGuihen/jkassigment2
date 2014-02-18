using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace Trip_booking.Models
{
    public class Trips
    {
        public int TripID { get; set; }
        public string Trip_Name { get; set; }
        public int LegID { get; set; }
        public  DateTime Start_Date { get; set; }
        public DateTime Finish_Date { get; set; }
        // [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]


        public ICollection<Trips> Trips { get; set; }
    }

    public class Legs
    {
        public int LegID { get; set; }
        public string Start_location { get; set; }
        public string Finish_location { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime Finish_Date { get; set; }
        
    }

    public class Guest
    {
        public string Guest_Name { get; set; }
        public int LegID { get; set; }
        

    }




}