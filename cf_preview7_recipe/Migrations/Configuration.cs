namespace cf_preview7_recipe.Migrations
{
    using cf_preview7_recipe.DTO;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<cf_preview7_recipe.Model>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(cf_preview7_recipe.Model context)
        {
            context.Dishes.AddRange(new Dish[]
            {
                new Dish { DishName = "Bun bo" },
                new Dish { DishName = "Nem ran"},
                new Dish { DishName = "Cha lua"},
                new Dish { DishName = "Com tam"},
            });

            context.Materials.AddRange(new Material[]
            {
                new Material { MaterialName = "Bun", Status = true},
                new Material { MaterialName = "Thit bo", Status = true},
                new Material { MaterialName = "Nuoc leo", Status = true},
                new Material { MaterialName = "Thit heo", Status = false},
                new Material { MaterialName = "Sa", Status = true},
                new Material { MaterialName = "Nuoc mam", Status = false},
            });
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
