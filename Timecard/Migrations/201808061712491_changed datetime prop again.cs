namespace Timecard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeddatetimepropagain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Signedinalready", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Timesheets", "Signin", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Timesheets", "Signin", c => c.DateTime(nullable: false));
            DropColumn("dbo.Students", "Signedinalready");
        }
    }
}
