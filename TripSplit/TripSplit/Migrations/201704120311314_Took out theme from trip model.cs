namespace TripSplit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tookoutthemefromtripmodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trips", "ThemeId", "dbo.Themes");
            DropIndex("dbo.Trips", new[] { "ThemeId" });
            DropColumn("dbo.Trips", "ThemeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trips", "ThemeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Trips", "ThemeId");
            AddForeignKey("dbo.Trips", "ThemeId", "dbo.Themes", "Id", cascadeDelete: true);
        }
    }
}
