namespace Timecard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeddatetimeprop : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Timesheets", "Signout", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Timesheets", "Signout", c => c.DateTime(nullable: false));
        }
    }
}
