using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;


namespace Trip_booking.DAL
{
    public class BookingContext :DbContext 
    {
        public DbSet<Trips> Trips { get; set; }
        public DbSet<Legs> legs { get; set; }
        public DbSet<Start_Date> Start_Date { get; set; }
        public DbSet<Finish_Date> Finish_Date { get; set; }
    }
    
    public BookingContext ()
        : base ("Booking_DataBase")
        {
            Database.SetInitializer(new BookingInitializer());
            }
 protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

}