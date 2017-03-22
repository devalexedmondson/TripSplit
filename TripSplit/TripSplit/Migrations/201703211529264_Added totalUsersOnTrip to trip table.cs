namespace TripSplit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedtotalUsersOnTriptotriptable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trips", "totalUsersOnTrip", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trips", "totalUsersOnTrip");
        }
    }
}
