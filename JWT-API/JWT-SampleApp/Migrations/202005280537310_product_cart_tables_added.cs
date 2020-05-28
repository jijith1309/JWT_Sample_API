namespace JWT_SampleApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class product_cart_tables_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        CartItemId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ShoppingCartId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CartItemId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.ShoppingCarts", t => t.ShoppingCartId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.ShoppingCartId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Category = c.String(maxLength: 20, storeType: "nvarchar"),
                        Name = c.String(maxLength: 50, storeType: "nvarchar"),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(maxLength: 100, storeType: "nvarchar"),
                        Quantity = c.Int(nullable: false),
                        LastUpdatedOn = c.DateTime(nullable: false, precision: 0),
                        ProductImagePath = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.ShoppingCarts",
                c => new
                    {
                        ShoppingCartId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        TotalShoppingCartPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedOn = c.DateTime(nullable: false, precision: 0),
                        UpdatedOn = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.ShoppingCartId)
                .ForeignKey("dbo.ApplicationUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItems", "ShoppingCartId", "dbo.ShoppingCarts");
            DropForeignKey("dbo.ShoppingCarts", "UserId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.CartItems", "ProductId", "dbo.Products");
            DropIndex("dbo.ShoppingCarts", new[] { "UserId" });
            DropIndex("dbo.CartItems", new[] { "ShoppingCartId" });
            DropIndex("dbo.CartItems", new[] { "ProductId" });
            DropTable("dbo.ShoppingCarts");
            DropTable("dbo.Products");
            DropTable("dbo.CartItems");
        }
    }
}
