using System;
using Bacola.Core.Entities;
using Bacola.Core.Entities.BaseEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bacola.Data.Contexts
{
	public class BacolaDbContext: IdentityDbContext<AppUser>
    {
            public BacolaDbContext(DbContextOptions<BacolaDbContext> options) : base(options)
            {

            }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Blog> Blogs { get; set; }
            public DbSet<Tag> Tags { get; set; }
            public DbSet<Brand> Brands { get; set; }
            public DbSet <Product> Products { get; set; }
            public DbSet<ProductImage> ProductImages { get; set; }
            public DbSet<HomeSlider> HomeSliders { get; set; }
            public DbSet<Setting> Settings { get; set; }
            public DbSet<Basket> Baskets { get; set; }
            public DbSet<BasketItem> BasketItems { get; set; }
            public DbSet<WishlistItem> WishlistItems { get; set; }
            public DbSet<Wishlist> Wishlists { get; set; }
            public DbSet<Contact> Contacts { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<OrderItem> OrderItems { get; set; }
            public DbSet<Coupon> Coupons { get; set; }
            public DbSet<Coment> Coments { get; set; }


            public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            {
                foreach (var entry in ChangeTracker.Entries<BaseEntity>())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.CreatedAt = DateTime.UtcNow.AddHours(4);
                            break;
                        case EntityState.Modified:
                            entry.Entity.UpdatedAt = DateTime.UtcNow.AddHours(4);
                            break;
                    }
                }
                return base.SaveChangesAsync(cancellationToken);
            }
      
    }

}

