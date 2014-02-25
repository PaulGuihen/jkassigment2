using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Trip_booking.Models;

namespace Trip_booking.DAL
{   

    public class GuestsContext : DbContext
    {
        public DbSet<Trips> Trips { get; set; }
        public DbSet<Legs> Legs { get; set; }
        public DbSet<legs_guest> legs_guest { get; set; }
        public DbSet<Guest> Guest { get; set; }

        public GuestsContext()
            : base("TripsDB")
        {
            Database.SetInitializer(new GuestInitializer());

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    public class GuestInitializer : DropCreateDatabaseIfModelChanges<GuestsContext>
    {
        protected override void Seed(GuestsContext context)
        {
            var guests = new List<Guest>
            {
            new Guest{Guest_Name="Carson"},
            new Guest{Guest_Name="Meredith"},
            new Guest{Guest_Name="Arturo"},
            new Guest{Guest_Name="Gytis"},
            new Guest{Guest_Name="Yan"},
            new Guest{Guest_Name="Peggy"},
            new Guest{Guest_Name="Laura"},
            new Guest{Guest_Name="Nino"}
            };
            guests.ForEach(s => context.Guest.Add(s));
            context.SaveChanges();


            var Trips = new List<Trips>
            {
            new Trips{TripID=1050,Trip_Name="America",LegID =10 ,Start_Date=DateTime.Parse("2001-09-01"),Finish_Date=DateTime.Parse("2001-16-01")},
            new Trips{TripID=1051,Trip_Name="England",LegID =11,Start_Date=DateTime.Parse("2001-17-01"),Finish_Date=DateTime.Parse("2001-24-01")},
            //new Trips{TripID=1052,Trip_Name="France",LegID =12,Start_Date=DateTime.Parse("2001-25-01"),Finish_Date=DateTime.Parse("2001-02-02")},
            new Trips{TripID=1050,Trip_Name="Ireland",LegID =13,Start_Date=DateTime.Parse("2001-03-02"),Finish_Date=DateTime.Parse("2001-10-02")},
         
            };
            Trips.ForEach(s => context.Trips.Add(s));
            context.SaveChanges();
            
            var Legs = new List<Legs>
            {
              new Legs{LegID=1001,Start_location="New_York",Finish_location="LosVegus", Start_Date=DateTime.Parse("2001-09-01"),Finish_Date=DateTime.Parse("2001-16-01"),TripID=30},
              new Legs{LegID=1002,Start_location="Canda",Finish_location="Niagra_Falls", Start_Date=DateTime.Parse("2001-17-01"),Finish_Date=DateTime.Parse("2001-24-01"),TripID=30},
              new Legs{LegID=1001,Start_location="Florida",Finish_location="California", Start_Date=DateTime.Parse("2001-25-01"),Finish_Date=DateTime.Parse("2001-02-02"),TripID=30},
           
              new Legs{LegID=1001,Start_location="Wales",Finish_location="Scotland", Start_Date=DateTime.Parse("2001-25-01"),Finish_Date=DateTime.Parse("2001-02-02"),TripID=30},
              new Legs{LegID=1001,Start_location="LiverPool",Finish_location="BlackPool", Start_Date=DateTime.Parse("2001-25-01"),Finish_Date=DateTime.Parse("2001-02-02"),TripID=30},
              new Legs{LegID=1001,Start_location="Cardiff",Finish_location="Portsmuth", Start_Date=DateTime.Parse("2001-25-01"),Finish_Date=DateTime.Parse("2001-02-02"),TripID=30},
           
               new Legs{LegID=1001,Start_location="Sligo",Finish_location="Galway", Start_Date=DateTime.Parse("2001-25-01"),Finish_Date=DateTime.Parse("2001-02-02"),TripID=30},
               new Legs{LegID=1001,Start_location="Dublin",Finish_location="Belfast", Start_Date=DateTime.Parse("2001-25-01"),Finish_Date=DateTime.Parse("2001-02-02"),TripID=30},
               new Legs{LegID=1001,Start_location="Limerick",Finish_location="WaterFord", Start_Date=DateTime.Parse("2001-25-01"),Finish_Date=DateTime.Parse("2001-02-02"),TripID=30},
           
          
            };
            Legs.ForEach(s => context.Legs.Add(s));
            
            context.SaveChanges();
        }
    }   // end SchoolInitializer class
}