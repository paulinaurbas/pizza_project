using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Models;
using PizzaApp.Models.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaApp.Data
{
    public class ApplicationDbContext
        : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<Dish>(dish =>
            {
                dish.HasIndex(d => d.Name).IsUnique();
            });

            builder.Entity<DishIngredient>(dishIngredient =>
            {
                dishIngredient.HasIndex(di => new { di.IngredientId, di.DishId }).IsUnique();
            });

            builder.Entity<Ingredient>(ingredient =>
            {
                ingredient.HasIndex(i => i.Name).IsUnique();
            });

            builder.Entity<MenuItem>(menuItem =>
            {
                menuItem.HasIndex(m => m.Name).IsUnique();
                menuItem.Property(m => m.Price).HasPrecision(2);
            });

            builder.Entity<MenuItemDish>(menuItemDish =>
            {
                menuItemDish.HasIndex(md => new { md.MenuItemId, md.DishId }).IsUnique();
                menuItemDish.Property(m => m.AdditionalPrice).HasPrecision(2);
            });

            builder.Entity<Restaurant>(restaurant =>
            {
                restaurant.HasIndex(r => r.Name).IsUnique();
                restaurant.HasIndex(r => r.Telephone).IsUnique();
                restaurant.HasIndex(r => r.Email).IsUnique();
            });


            builder.Entity<RestaurantIngredient>(restaurantIngredient =>
            {
                restaurantIngredient.HasIndex(ri => new { ri.RestaurantId, ri.IngredientId }).IsUnique();
            });

            builder.Entity<RestaurantMenuItem>(restaurantMenuItem =>
            {
                restaurantMenuItem.HasIndex(rm => new { rm.RestaurantId, rm.MenuItemId }).IsUnique();
            });

            builder.Entity<OrderStatus>(orderStatus =>
            {
                orderStatus.HasIndex(os => os.Name).IsUnique();
            });

            builder.Entity<OrderMenuItemDish>(orderMenuItemDish =>
            {
                orderMenuItemDish.HasIndex(omd => new { omd.OrderMenuItemId, omd.MenuItemDishId }).IsUnique();
            });

            builder.Entity<ReservationRestaurantTable>(reservationRestaurantTable =>
            {
                reservationRestaurantTable.HasIndex(rrt => new { rrt.ReservationId, rrt.RestaurantTableId }).IsUnique();
            });

            builder.Entity<ReservationStatus>(reservationStatus =>
            {
                reservationStatus.HasIndex(rs => rs.Name).IsUnique();
            });


            builder.Entity<RestaurantTable>(restaurantTable =>
            {
                restaurantTable.HasIndex(rt => new { rt.RestaurantId, rt.Name }).IsUnique();
            });
        }

        public DbSet<Wholesaler> Wholesalers { get; set; }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<DishIngredient> DishIngredients { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<MenuItemDish> MenuItemDishes { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantIngredient> RestaurantIngredients { get; set; }
        public DbSet<RestaurantMenuItem> RestaurantMenuItems { get; set; }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderMenuItem> OrderMenuItems { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<OrderMenuItemDish> OrderMenuItemDishes { get; set; }

        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationRestaurantTable> ReservationRestaurantTables { get; set; }
        public DbSet<ReservationStatus> ReservationStatuses { get; set; }
        public DbSet<RestaurantTable> RestaurantTables { get; set; }
    }
}
