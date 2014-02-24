using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Trip_booking.Models
{
    public class Trips
    {
        [Key]
        public int TripID { get; set; }
        public string Trip_Name { get; set; }
        public int LegID { get; set; }
        public  DateTime Start_Date { get; set; }
        public DateTime Finish_Date { get; set; }
        // [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]


        public virtual ICollection<Legs> legs { get; set; }
    }

    public class Legs
    {
        [Key]
        public int LegID { get; set; }
        public string Start_location { get; set; }
        public string Finish_location { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime Finish_Date { get; set; }
        public int TripID { get; set; }

        public virtual ICollection<legs_guest> legs_guest { get; set; }
    }

    public class legs_guest
    {
        [Key]
        public int ID { get; set; }
        public int LegID { get; set; }
        public int GuestID { get; set; }

        public virtual Legs legs { get; set; }

    }

    public class Guest
    {
        [Key]
        public int GuestID { get; set; }
        public string Guest_Name { get; set; }
        public int LegID { get; set; }

        public virtual Guest Guests { get; set; }
        

    }

    public class TripsDBContext : DbContext
    {


        static TripsDBContext()
        {
            Database.SetInitializer<Models.TripsDBContext>(null);
        }



        public DbSet<Trips> TripsDbSet { get; set; }
        public DbSet<Legs> LegsDbSet { get; set; }
        public DbSet<legs_guest> legs_guestDbSet { get; set; }
        public DbSet<Guest> GuestDbSet { get; set; }

    }

    


}