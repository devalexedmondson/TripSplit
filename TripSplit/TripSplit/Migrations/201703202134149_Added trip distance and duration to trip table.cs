namespace TripSplit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedtripdistanceanddurationtotriptable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trips", "tripDistance", c => c.Double(nullable: false));
            AddColumn("dbo.Trips", "tripDuration", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trips", "tripDuration");
            DropColumn("dbo.Trips", "tripDistance");
        }
    }
}
