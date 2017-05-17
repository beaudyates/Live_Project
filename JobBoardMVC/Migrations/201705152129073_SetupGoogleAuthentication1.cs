namespace JobBoardMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetupGoogleAuthentication1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.User", "Resume");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "Resume", c => c.Byte(nullable: false));
        }
    }
}
