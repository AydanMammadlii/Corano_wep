using Corona.Application.Abstraction.Repositories.IEntityRepository;
using Corona.Domain.Entities;
using Corona.Persistance.Context;

namespace Corona.Persistance.Implementations.Repositories.IEntityRepository;

public class ProductTypeReadRepository : ReadRepository<ProductType>, IProductTypeReadRepository
{
    public ProductTypeReadRepository(AppDbContext context) : base(context)
    {
    }
}
