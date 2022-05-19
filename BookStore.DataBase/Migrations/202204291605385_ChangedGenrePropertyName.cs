namespace BookStore.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedGenrePropertyName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "BookGenre", c => c.Int());
            AddColumn("dbo.Products", "JournalGenre", c => c.Int());
            DropColumn("dbo.Products", "Genres");
            DropColumn("dbo.Products", "Genres1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Genres1", c => c.Int());
            AddColumn("dbo.Products", "Genres", c => c.Int());
            DropColumn("dbo.Products", "JournalGenre");
            DropColumn("dbo.Products", "BookGenre");
        }
    }
}
