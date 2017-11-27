namespace MCDataBookLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyMigration14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        bookId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 300),
                        ReleaseDate = c.DateTime(),
                        AuthorId = c.Int(nullable: false),
                        WriterId = c.Int(),
                        GenreId = c.Int(),
                        Description = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.bookId)
                .ForeignKey("dbo.Genres", t => t.GenreId)
                .ForeignKey("dbo.Writers", t => t.WriterId)
                .Index(t => t.WriterId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GenreName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Writers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 200),
                        LastName = c.String(maxLength: 200),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "WriterId", "dbo.Writers");
            DropForeignKey("dbo.Books", "GenreId", "dbo.Genres");
            DropIndex("dbo.Books", new[] { "GenreId" });
            DropIndex("dbo.Books", new[] { "WriterId" });
            DropTable("dbo.Writers");
            DropTable("dbo.Genres");
            DropTable("dbo.Books");
        }
    }
}
