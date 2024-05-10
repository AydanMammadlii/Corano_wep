using Corona.Application.Abstraction.Repositories.IEntityRepository;
using Corona.Domain.Entities;
using Corona.Persistance.Context;

namespace Corona.Persistance.Implementations.Repositories.IEntityRepository;

public class BrandReadRepository : ReadRepository<Brand>, IBrandReadRepository
{
    public BrandReadRepository(AppDbContext context) : base(context)
    {
    }
}