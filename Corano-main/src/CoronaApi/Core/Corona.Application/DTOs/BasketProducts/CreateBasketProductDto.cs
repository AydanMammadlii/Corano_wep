namespace Corona.Application.DTOs.BasketProducts;

public class CreateBasketProductDto
{
    public Guid ProductId { get; set; }
    public Guid BasketId { get; set; }
    public int Count { get; set; } = 1;
}