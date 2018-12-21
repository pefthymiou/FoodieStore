namespace FoodieStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productnamestringlength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "ProductName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "ProductName", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
