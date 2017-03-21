namespace TripSplit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedIspublictostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trips", "IsPublic", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trips", "IsPublic", c => c.Boolean(nullable: false));
        }
    }
}
