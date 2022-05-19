namespace BookStore.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Start : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Description = c.String(),
                        PublicationDate = c.DateTime(nullable: false),
                        BasePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AuthorName = c.String(),
                        Title = c.String(),
                        Edition = c.Int(),
                        Synopsis = c.String(),
                        EditorName = c.String(),
                        Name = c.String(),
                        IssueNumber = c.Int(),
                        Frequency = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
        }
    }
}
