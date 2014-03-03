namespace Trip_booking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test_Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Guests",
                c => new
                    {
                        GuestID = c.Int(nullable: false, identity: true),
                        Guest_Name = c.String(),
                        LegID = c.Int(nullable: false),
                        Guests_GuestID = c.Int(),
                    })
                .PrimaryKey(t => t.GuestID)
                .ForeignKey("dbo.Guests", t => t.Guests_GuestID)
                .Index(t => t.Guests_GuestID);
            
            CreateTable(
                "dbo.legs_guest",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LegID = c.Int(nullable: false),
                        GuestID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Legs", t => t.LegID, cascadeDelete: true)
                .Index(t => t.LegID);
            
            CreateTable(
                "dbo.Legs",
                c => new
                    {
                        LegID = c.Int(nullable: false, identity: true),
                        Start_location = c.String(),
                        Finish_location = c.String(),
                        Start_Date = c.DateTime(nullable: false),
                        Finish_Date = c.DateTime(nullable: false),
                        TripID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LegID)
                .ForeignKey("dbo.Trips", t => t.TripID, cascadeDelete: true)
                .Index(t => t.TripID);
            
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        TripID = c.Int(nullable: false, identity: true),
                        Trip_Name = c.String(),
                        LegID = c.Int(nullable: false),
                        Start_Date = c.DateTime(nullable: false),
                        Finish_Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TripID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Legs", "TripID", "dbo.Trips");
            DropForeignKey("dbo.legs_guest", "LegID", "dbo.Legs");
            DropForeignKey("dbo.Guests", "Guests_GuestID", "dbo.Guests");
            DropIndex("dbo.Legs", new[] { "TripID" });
            DropIndex("dbo.legs_guest", new[] { "LegID" });
            DropIndex("dbo.Guests", new[] { "Guests_GuestID" });
            DropTable("dbo.Trips");
            DropTable("dbo.Legs");
            DropTable("dbo.legs_guest");
            DropTable("dbo.Guests");
        }
    }
}
