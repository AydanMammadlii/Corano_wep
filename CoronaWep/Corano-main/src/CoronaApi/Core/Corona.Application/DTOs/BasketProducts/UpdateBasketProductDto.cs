namespace Corona.Application.DTOs.BasketProducts;

public class UpdateBasketProductDto
{
    public Guid ProductId { get; set; }
    public Guid BasketId { get; set; }
    public int Count { get; set; } = 0;
}
