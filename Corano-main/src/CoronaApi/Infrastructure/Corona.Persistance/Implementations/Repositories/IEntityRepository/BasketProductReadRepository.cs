using Corona.Application.Abstraction.Repositories.IEntityRepository;
using Corona.Domain.Entities;
using Corona.Persistance.Context;

namespace Corona.Persistance.Implementations.Repositories.IEntityRepository;

public class BasketProductReadRepository : ReadRepository<BasketProduct>, IBasketProductReadRepository
{
    public BasketProductReadRepository(AppDbContext context) : base(context)
    {
    }
}