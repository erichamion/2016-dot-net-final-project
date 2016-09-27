namespace Performance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration009 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DailyStatLists", "TalkTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DailyStatLists", "TalkTime", c => c.Int(nullable: false));
        }
    }
}
