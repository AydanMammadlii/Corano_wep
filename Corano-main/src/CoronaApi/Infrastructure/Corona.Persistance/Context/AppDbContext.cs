using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Corona.Domain.Entities;
using Corona.Persistance.Configurations;
using System.Reflection.Metadata;

namespace Corona.Persistance.Context;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SliderConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<Testimonials> Testimonials { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BasketProduct> BasketProducts { get; set; }
    public DbSet<Wishlist> Wishlists { get; set; }
    public DbSet<WishlistProduct> WishlistProducts { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<BlogImage> BlogImages { get; set; }
    public DbSet<Comment> Comments { get; set; }


    public DbSet<Movie> Movies { get; set; }
}
