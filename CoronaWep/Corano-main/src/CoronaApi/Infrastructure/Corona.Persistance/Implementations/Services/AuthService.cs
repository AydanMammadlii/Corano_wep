using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Corona.Application.Abstraction.Services;
using Corona.Application.DTOs.Auth;
using Corona.Domain.Entities;
using Corona.Domain.Enums;
using Corona.Domain.Helpers;
using Corona.Persistance.Context;
using Corona.Persistance.Exceptions;
using System.Text;

namespace Corona.Persistance.Implementations.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _siginManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly ITokenHandler _tokenHandler;

    public AuthService(UserManager<AppUser> userManager,
                       SignInManager<AppUser> siginManager,
                       RoleManager<IdentityRole> roleManager,
                       AppDbContext context,
                       IConfiguration configuration,
                       ITokenHandler tokenHandler)
    {
        _userManager = userManager;
        _siginManager = siginManager;
        _roleManager = roleManager;
        _context = context;
        _configuration = configuration;
        _tokenHandler = tokenHandler;
    }

    public async Task AdminCreate(AdminCreate adminCreate)
    {
        var bySuperAdmin = await _userManager.FindByIdAsync(adminCreate.superAdminId);
        if (bySuperAdmin is null) throw new NotFoundException("SuperAdmin Not found");
        if (bySuperAdmin == null || !await _userManager.IsInRoleAsync(bySuperAdmin, "SuperAdmin"))
            throw new UnauthorizedAccessException("You do not have permission to perform this action.");


        var targetUser = await _userManager.FindByIdAsync(adminCreate.appUserId);

        if (targetUser == null) throw new NotFoundException("User Not Found");

        await _userManager.RemoveFromRoleAsync(targetUser, "Member");
        await _userManager.AddToRoleAsync(targetUser, "Admin");
    }


    public async Task AdminDelete(AdminCreate adminCreate)
    {
        var bySuperAdmin = await _userManager.FindByIdAsync(adminCreate.superAdminId);
        if (bySuperAdmin is null) throw new NotFoundException("SuperAdmin Not found");
        if (bySuperAdmin == null || !await _userManager.IsInRoleAsync(bySuperAdmin, "SuperAdmin"))
            throw new UnauthorizedAccessException("You do not have permission to perform this action.");


        var targetAdmin = await _userManager.FindByIdAsync(adminCreate.appUserId);

        if (targetAdmin == null) throw new NotFoundException("Admin Not Found");

        await _userManager.RemoveFromRoleAsync(targetAdmin, "Admin");
        await _userManager.AddToRoleAsync(targetAdmin, "Member");
    }

    public async Task<List<AppUser>> AllAdminUser()
    {
        IQueryable<AppUser> AllUsers = _context.Users;

        var AdminList = new List<AppUser>();
        foreach (var item in await AllUsers.ToListAsync())
        {
            var userRoles = await _userManager.GetRolesAsync(item);
            if (userRoles.Contains("Admin"))
            {
                AdminList.Add(item);
            }
        }
        return AdminList;
    }


    public async Task<List<AppUser>> AllMemberUser()
    {
        IQueryable<AppUser> AllUsers = _context.Users;

        var MemberList = new List<AppUser>();
        foreach (var item in await AllUsers.ToListAsync())
        {
            var userRoles = await _userManager.GetRolesAsync(item);
            if (userRoles.Contains("Member"))
            {
                MemberList.Add(item);
            }
        }
        return MemberList;
    }
    public async Task<TokenResponseDTO> Login(LoginDTO loginDTO)
    {
        AppUser appUser = await _userManager.FindByEmailAsync(loginDTO.UsernameOrEmail);
        if (appUser is null)
        {
            appUser = await _userManager.FindByNameAsync(loginDTO.UsernameOrEmail);
            if (appUser is null) throw new LogInFailerException("Invalid Login!");

        }

        SignInResult signInResult = await _siginManager.CheckPasswordSignInAsync(appUser, loginDTO.password, true);
        if (!signInResult.Succeeded)
        {
            throw new LogInFailerException("Invalid Login!");
        }

        var basketId = await _context.Baskets.Where(x => x.AppUserId == appUser.Id).Select(x => x.Id).FirstAsync();

        var tokenRespons = await _tokenHandler.CreateAccessToken(2, 3, appUser, basketId);
        appUser.RefreshToken = tokenRespons.refreshToken;
        appUser.RefreshTokenExpration = tokenRespons.refreshTokenExpration;
        await _userManager.UpdateAsync(appUser);

        var appUserWishlistAndBasket = await _context.AppUsers
                                        .Include(x => x.Basket)
                                        .Include(x => x.Wishlist)
                                        .FirstOrDefaultAsync(x => x.Id == appUser.Id);

        return tokenRespons;
    }

    public Task<TokenResponseDTO> LoginAdmin(LoginDTO loginDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<SignUpResponse> Register(RegisterDTO registerDTO)
    {
        AppUser appUser = new AppUser()
        {
            Fullname = registerDTO.Fullname,
            UserName = registerDTO.Username,
            Email = registerDTO.Email,
            BirthDate = registerDTO.BirthDate,
            isActive = false
        };

        IdentityResult identityResult = await _userManager.CreateAsync(appUser, registerDTO.password);
        if (!identityResult.Succeeded)
        {
            StringBuilder errorMessage = new();
            foreach (var error in identityResult.Errors)
            {
                errorMessage.AppendLine(error.Description);
            }
            throw new RegistrationException(errorMessage.ToString());
        }

        var result = await _userManager.AddToRoleAsync(appUser, Role.Member.ToString());
        if (!result.Succeeded)
        {
            return new SignUpResponse
            {
                StatusMessage = ExceptionResponseMessages.UserFailedMessage,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }

        var myBasket = new Basket()
        {
            AppUserId = appUser.Id
        };
        var wishlist = new Wishlist()
        {
            AppUserId = appUser.Id
        };

        await _context.Wishlists.AddAsync(wishlist);
        await _context.Baskets.AddAsync(myBasket);
        await _context.SaveChangesAsync();

        return new SignUpResponse
        {
            Errors = null,
            StatusMessage = ExceptionResponseMessages.UserSuccesMessage,
            UserEmail = appUser.Email,
            Password = registerDTO.password
        };
    }

    public async Task<TokenResponseDTO> ValidRefleshToken(string refreshToken)
    {
        if (refreshToken is null)
        {
            throw new ArgumentNullException("Refresh token does not exist");
        }
        var ByUser = await _context.Users.Include(x => x.Basket).Where(a => a.RefreshToken == refreshToken).FirstOrDefaultAsync();
        if (ByUser is null)
        {
            throw new NotFoundException("User does Not Exist");
        }
        if (ByUser.RefreshTokenExpration < DateTime.UtcNow)
        {
            throw new ArgumentNullException("Refresh token does not exist");
        }

        var tokenResponse = await _tokenHandler.CreateAccessToken(2, 3, ByUser, ByUser.Basket.Id);
        ByUser.RefreshToken = tokenResponse.refreshToken;
        ByUser.RefreshTokenExpration = tokenResponse.refreshTokenExpration;
        await _userManager.UpdateAsync(ByUser);
        return tokenResponse;
    }
}
