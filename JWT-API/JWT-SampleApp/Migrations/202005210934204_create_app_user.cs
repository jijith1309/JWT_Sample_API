namespace JWT_SampleApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_app_user : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(unicode: false),
                        Password = c.String(unicode: false),
                        FirstName = c.String(unicode: false),
                        LastName = c.String(unicode: false),
                        EmailId = c.String(unicode: false),
                        PhoneNumber = c.String(unicode: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ApplicationUsers");
        }
    }
}
