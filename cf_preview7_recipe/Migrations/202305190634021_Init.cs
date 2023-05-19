namespace cf_preview7_recipe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dishes",
                c => new
                    {
                        DishId = c.Int(nullable: false, identity: true),
                        DishName = c.String(),
                    })
                .PrimaryKey(t => t.DishId);
            
            CreateTable(
                "dbo.DishMaterials",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 5),
                        Number = c.Int(nullable: false),
                        Unit = c.String(),
                        DishId = c.Int(nullable: false),
                        MaterialId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dishes", t => t.DishId, cascadeDelete: true)
                .ForeignKey("dbo.Materials", t => t.MaterialId, cascadeDelete: true)
                .Index(t => t.DishId)
                .Index(t => t.MaterialId);
            
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        MaterialId = c.Int(nullable: false, identity: true),
                        MaterialName = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MaterialId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DishMaterials", "MaterialId", "dbo.Materials");
            DropForeignKey("dbo.DishMaterials", "DishId", "dbo.Dishes");
            DropIndex("dbo.DishMaterials", new[] { "MaterialId" });
            DropIndex("dbo.DishMaterials", new[] { "DishId" });
            DropTable("dbo.Materials");
            DropTable("dbo.DishMaterials");
            DropTable("dbo.Dishes");
        }
    }
}
