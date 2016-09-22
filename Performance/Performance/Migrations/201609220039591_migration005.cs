namespace Performance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration005 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyStatLists", "HandleTime", c => c.Int(nullable: false));
            AddColumn("dbo.DailyStatLists", "TalkTime", c => c.Int(nullable: false));
            AddColumn("dbo.DailyStatLists", "HoldTime", c => c.Int(nullable: false));
            AddColumn("dbo.DailyStatLists", "WorkTime", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DailyStatLists", "WorkTime");
            DropColumn("dbo.DailyStatLists", "HoldTime");
            DropColumn("dbo.DailyStatLists", "TalkTime");
            DropColumn("dbo.DailyStatLists", "HandleTime");
        }
    }
}
