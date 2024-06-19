namespace Corona.Application.DTOs.WishlistDto;

public class RemoveWishlistProductDto
{
    public string AppUserId { get; set; }
    public Guid ProductId { get; set; }
}
