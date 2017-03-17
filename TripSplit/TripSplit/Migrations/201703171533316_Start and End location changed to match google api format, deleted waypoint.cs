namespace TripSplit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StartandEndlocationchangedtomatchgoogleapiformatdeletedwaypoint : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trips", "originInput", c => c.String());
            AddColumn("dbo.Trips", "destinationInput", c => c.String());
            DropColumn("dbo.Trips", "StartTrip");
            DropColumn("dbo.Trips", "EndTrip");
            DropColumn("dbo.Trips", "Waypoint");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trips", "Waypoint", c => c.String());
            AddColumn("dbo.Trips", "EndTrip", c => c.String());
            AddColumn("dbo.Trips", "StartTrip", c => c.String());
            DropColumn("dbo.Trips", "destinationInput");
            DropColumn("dbo.Trips", "originInput");
        }
    }
}
