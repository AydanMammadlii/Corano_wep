using Corona.Application.Abstraction.Repositories.IEntityRepository;
using Corona.Domain.Entities;
using Corona.Persistance.Context;

namespace Corona.Persistance.Implementations.Repositories.IEntityRepository;

public class WishlistProductReadRepository : ReadRepository<WishlistProduct>, IWishlistProductReadRepository
{
    public WishlistProductReadRepository(AppDbContext context) : base(context)
    {
    }
}