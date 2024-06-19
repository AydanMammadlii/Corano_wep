namespace Corona.Application.DTOs.BasketProducts;

public class GetBasketProductDto
{
    public Guid ProductId { get; set; }
    public Guid BasketId { get; set; }
    public int Count { get; set; }
    public double Price { get; set; }
    public string ImageUrl { get; set; }
    public string ProductTitle { get; set; }
}
