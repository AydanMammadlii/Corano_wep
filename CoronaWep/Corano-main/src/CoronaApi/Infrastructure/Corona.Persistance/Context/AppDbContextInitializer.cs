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
    }
}
