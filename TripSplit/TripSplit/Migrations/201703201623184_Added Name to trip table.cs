namespace TripSplit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNametotriptable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trips", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trips", "Name");
        }
    }
}
