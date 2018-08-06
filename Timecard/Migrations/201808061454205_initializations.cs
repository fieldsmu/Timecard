namespace Timecard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initializations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Pin = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Timesheets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Signin = c.DateTime(nullable: false),
                        Signout = c.DateTime(nullable: false),
                        StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Timesheets", "StudentId", "dbo.Students");
            DropIndex("dbo.Timesheets", new[] { "StudentId" });
            DropTable("dbo.Timesheets");
            DropTable("dbo.Students");
        }
    }
}
