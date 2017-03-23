namespace TripSplit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeddepartureandreturndatetotriptable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trips", "departureDate", c => c.String());
            AddColumn("dbo.Trips", "returnDate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trips", "returnDate");
            DropColumn("dbo.Trips", "departureDate");
        }
    }
}
