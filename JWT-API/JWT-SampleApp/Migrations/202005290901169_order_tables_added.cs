namespace JWT_SampleApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class order_tables_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailsId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderDetailsId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        TotalOrderPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Address1 = c.String(unicode: false),
                        Address2 = c.String(unicode: false),
                        City = c.String(maxLength: 30, storeType: "nvarchar"),
                        Country = c.String(maxLength: 30, storeType: "nvarchar"),
                        PostalCode = c.String(maxLength: 30, storeType: "nvarchar"),
                        PhoneNumber = c.String(maxLength: 30, storeType: "nvarchar"),
                        OrderStatus = c.String(maxLength: 15, storeType: "nvarchar"),
                        CreatedOn = c.DateTime(nullable: false, precision: 0),
                        UpdatedOn = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.ApplicationUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "UserId", "dbo.ApplicationUsers");
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
        }
    }
}
