namespace TripSplit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedflightnumbertotrip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trips", "flightNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trips", "flightNumber");
        }
    }
}
