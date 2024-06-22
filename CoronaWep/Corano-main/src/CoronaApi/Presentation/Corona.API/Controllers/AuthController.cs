using Corona.Application.Abstraction.Services;
using Corona.Application.DTOs.Auth;
using Corona.Domain.Entities;
using Corona.Domain.Helpers;
using Corona.Persistance.Context;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Corona.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly AppDbContext _appDbContext;

    public AuthController(IAuthService authService, AppDbContext appDbContext)
    {
        _authService = authService;
        _appDbContext = appDbContext;

    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        var responseToken = await _authService.Login(loginDTO);
        return Ok(responseToken);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
    {
        ArgumentNullException.ThrowIfNull(registerDTO, ExceptionResponseMessages.ParametrNotFoundMessage);

        SignUpResponse response = await _authService.Register(registerDTO)
                ?? throw new SystemException(ExceptionResponseMessages.NotFoundMessage);

        if (response.Errors != null)
        {
            if (response.Errors.Count > 0)
            {
                return BadRequest(response.Errors);
            }
        }
        else
        {
            //string subject = "Register Confirmation";
            //string html = string.Empty;
            //string password = registerDTO.password;

            //string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "verify.html");
            //html = System.IO.File.ReadAllText(filePath);

            //html = html.Replace("{{password}}", password);

            //_emailService.Send(registerDTO.Email, subject, html);

        }
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> RefreshToken([FromQuery] string ReRefreshtoken)
    {
        var response = await _authService.ValidRefleshToken(ReRefreshtoken);
        return Ok(response);
    }


    [HttpGet("AllMember")]
    public async Task<IActionResult> AllMemberUssers()
    {
        var memberUsers = await _authService.AllMemberUser();
        return Ok(memberUsers);
    }

    [HttpGet("AllAdmin")]
    public async Task<IActionResult> AllAdminUsers()
    {
        var adminUsers = await _authService.AllAdminUser();
        return Ok(adminUsers);
    }

    [HttpPost("AdminCreate")]
    public async Task<IActionResult> AdminCreate(AdminCreate adminCreate)
    {
        await _authService.AdminCreate(adminCreate);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPost("AdminDelete")]
    public async Task<IActionResult> AdminDelete(AdminCreate adminCreate)
    {
        await _authService.AdminDelete(adminCreate);
        return Ok();
    }

    [HttpPost("BasketAndWishlist")]
    public async Task<IActionResult> Adminbasket(string appUserId)
    {
        var myBasket = new Basket()
        {
            AppUserId = appUserId
        };
        var wishlist = new Wishlist()
        {
            AppUserId = appUserId
        };

        await _appDbContext.Wishlists.AddAsync(wishlist);
        await _appDbContext.Baskets.AddAsync(myBasket);
        await _appDbContext.SaveChangesAsync();
        return Ok();
    }


}
