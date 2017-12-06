namespace MCDataBookLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Url = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Books", "PictureId", c => c.Int());
            CreateIndex("dbo.Books", "PictureId");
            AddForeignKey("dbo.Books", "PictureId", "dbo.Pictures", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "PictureId", "dbo.Pictures");
            DropIndex("dbo.Books", new[] { "PictureId" });
            DropColumn("dbo.Books", "PictureId");
            DropTable("dbo.Pictures");
        }
    }
}
