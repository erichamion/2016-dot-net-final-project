namespace Performance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration007 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.DailyStatLists");
            AlterColumn("dbo.DailyStatLists", "Date", c => c.DateTime(nullable: false));
            AddPrimaryKey("dbo.DailyStatLists", new[] { "EmployeeId", "Date" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.DailyStatLists");
            AlterColumn("dbo.DailyStatLists", "Date", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddPrimaryKey("dbo.DailyStatLists", new[] { "EmployeeId", "Date" });
        }
    }
}
