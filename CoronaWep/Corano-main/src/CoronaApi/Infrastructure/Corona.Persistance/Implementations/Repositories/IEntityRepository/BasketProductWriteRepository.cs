using Corona.Application.Abstraction.Repositories.IEntityRepository;
using Corona.Domain.Entities;
using Corona.Persistance.Context;

namespace Corona.Persistance.Implementations.Repositories.IEntityRepository;

public class BasketProductWriteRepository : WriteRepository<BasketProduct>, IBasketProductWriteRepository
{
    public BasketProductWriteRepository(AppDbContext context) : base(context)
    {
    }
}
