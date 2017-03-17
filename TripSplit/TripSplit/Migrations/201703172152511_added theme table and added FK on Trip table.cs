namespace TripSplit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedthemetableandaddedFKonTriptable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Themes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        destinationTheme = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Trips", "ThemeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Trips", "ThemeId");
            AddForeignKey("dbo.Trips", "ThemeId", "dbo.Themes", "Id", cascadeDelete: true);
            DropColumn("dbo.Trips", "Theme");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trips", "Theme", c => c.String());
            DropForeignKey("dbo.Trips", "ThemeId", "dbo.Themes");
            DropIndex("dbo.Trips", new[] { "ThemeId" });
            DropColumn("dbo.Trips", "ThemeId");
            DropTable("dbo.Themes");
        }
    }
}
