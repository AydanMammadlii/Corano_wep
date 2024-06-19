using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Corona.Domain.Entities;
using Corona.Domain.Enums;

namespace Corona.Persistance.Context;

public class AppDbContextInitializer
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AppDbContextInitializer(AppDbContext context,
                                   UserManager<AppUser> userManager,
                                   RoleManager<IdentityRole> roleManager,
                                   IConfiguration configuration)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task InitializeAsync()
    {
        await _context.Database.MigrateAsync();
    }

    public async Task RoleSeedAsync()
    {
        foreach (var role in Enum.GetValues(typeof(Role)))
        {
            if (!await _roleManager.RoleExistsAsync(role.ToString()))
            {
                await _roleManager.CreateAsync(new() { Name = role.ToString() });
            }
        }
    }

    public async Task UserSeedAsync()
    {
        AppUser appUser = new()
        {
            UserName = _configuration["SuperAdminSettings:username"],
            Email = _configuration["SuperAdminSettings:email"]
        };
        await _userManager.CreateAsync(appUser, _configuration["SuperAdminSettings:password"]);  //error bu derse baxs
        await _userManager.AddToRoleAsync(appUser, Role.SuperAdmin.ToString());

        //var appUserWishlistAndBasket = await _context.AppUsers
        //                         .Include(x => x.Basket)
        //                         .Include(x => x.Wishlist)
        //                         .FirstOrDefaultAsync(x => x.Id == appUser.Id);

        //if (appUserWishlistAndBasket.Wishlist is null)
        //{
        //    var wishlist = new Wishlist()
        //    {
        //        AppUserId = appUser.Id
        //    };
        //    await _context.Wishlists.AddAsync(wishlist);
        //}
        //if (appUserWishlistAndBasket.Basket is null)
        //{
        //    var myBasket = new Basket()
        //    {
        //        AppUserId = appUser.Id
        //    };
        //    await _context.Baskets.AddAsync(myBasket);
        //}
        //await _context.SaveChangesAsync();
    }
}
