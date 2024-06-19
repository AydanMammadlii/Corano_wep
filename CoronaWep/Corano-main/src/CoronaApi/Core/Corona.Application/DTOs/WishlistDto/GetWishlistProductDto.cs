namespace Corona.Application.DTOs.WishlistDto;

public class GetWishlistProductDto
{
    public Guid ProductId { get; set; }
    public Guid WishlistId { get; set; }
    public double Price { get; set; }
    public string ImageUrl { get; set; }
    public string ProductTitle { get; set; }
}
