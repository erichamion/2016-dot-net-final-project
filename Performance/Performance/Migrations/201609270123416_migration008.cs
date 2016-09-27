namespace Performance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration008 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Employees", name: "Manager_Id", newName: "managerId");
            RenameIndex(table: "dbo.Employees", name: "IX_Manager_Id", newName: "IX_managerId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Employees", name: "IX_managerId", newName: "IX_Manager_Id");
            RenameColumn(table: "dbo.Employees", name: "managerId", newName: "Manager_Id");
        }
    }
}
