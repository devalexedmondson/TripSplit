namespace TripSplit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeddepartureandreturndatetypestoDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trips", "departureDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Trips", "returnDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trips", "returnDate", c => c.String());
            AlterColumn("dbo.Trips", "departureDate", c => c.String());
        }
    }
}
