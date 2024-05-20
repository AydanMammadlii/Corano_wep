using Microsoft.AspNetCore.Identity;

namespace Corona.Domain.Entities;

public class AppUser:IdentityUser
{
    public string? Fullname { get; set; }
    public bool isActive { get; set; }
    public DateTime? BirthDate { get; set; }
    public byte[]? ImagePath { get; set; }
    public DateTime RefreshTokenExpration { get; set; }
    public string? RefreshToken { get; set; }
    public Basket? Basket { get; set; }
    public Wishlist? Wishlist { get; set; }
}
