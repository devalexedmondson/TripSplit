namespace TripSplit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tripmodeladded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        IsPublic = c.Boolean(nullable: false),
                        StartTrip = c.String(),
                        EndTrip = c.String(),
                        Waypoint = c.String(),
                        Cost = c.Double(nullable: false),
                        Theme = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationUserTrips",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Trip_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Trip_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Trips", t => t.Trip_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Trip_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserTrips", "Trip_Id", "dbo.Trips");
            DropForeignKey("dbo.ApplicationUserTrips", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserTrips", new[] { "Trip_Id" });
            DropIndex("dbo.ApplicationUserTrips", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserTrips");
            DropTable("dbo.Trips");
        }
    }
}
