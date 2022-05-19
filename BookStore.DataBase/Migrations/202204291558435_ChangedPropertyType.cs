namespace BookStore.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedPropertyType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Genres", c => c.Int());
            AddColumn("dbo.Products", "Genres1", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Genres1");
            DropColumn("dbo.Products", "Genres");
        }
    }
}
