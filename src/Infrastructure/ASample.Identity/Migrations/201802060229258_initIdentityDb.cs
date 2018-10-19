namespace ASample.Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initIdentityDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IdentityUser",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 20),
                        PasswordHash = c.String(nullable: false, maxLength: 1000),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleteTime = c.DateTime(),
                        CreateTime = c.DateTime(nullable: false),
                        ModifyTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.IdentityUser");
        }
    }
}
