namespace Warehouse.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Warehouse.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Warehouse.Models.WarehouseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Warehouse.Models.WarehouseContext";
        }

        protected override void Seed(Models.WarehouseContext db)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            db.Products.AddOrUpdate(
              p => p.Name, // Seed-identifierare (används för att kontrollera unikhet, vi betraktar Name som unik identifierare)
              new Product { Name = "Toaster 2010", Price = 130, Quantity=12, Category="appliances" },
              new Product { Name = "Waffle NG", Price = 12, Quantity = 33, Category = "appliances" },
              new Product { Name = "Tribolite 2", Price = 30, Quantity = 44, Category = "electronics" }
            );

        }
    }
}
