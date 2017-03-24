namespace TripSplit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedfriendemailnumbertotrip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trips", "friendEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trips", "friendEmail");
        }
    }
}
