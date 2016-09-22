namespace Performance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration004 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyStatLists", "Calls", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DailyStatLists", "Calls");
        }
    }
}
