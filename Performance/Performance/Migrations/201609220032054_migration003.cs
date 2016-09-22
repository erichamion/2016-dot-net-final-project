namespace Performance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration003 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DailyStatLists",
                c => new
                    {
                        EmployeeId = c.String(nullable: false, maxLength: 128),
                        Date = c.DateTime(nullable: false),
                        AttendancePoints = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmployeeId, t.Date })
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DailyStatLists", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.DailyStatLists", new[] { "EmployeeId" });
            DropTable("dbo.DailyStatLists");
        }
    }
}
